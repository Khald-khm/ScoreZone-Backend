using ScoreZone.Application.Auth;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Domain.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.User.Player;
using ScoreZone.Domain.User.Admin;
using ScoreZone.Domain.User.Owner;
using Microsoft.EntityFrameworkCore;

namespace ScoreZone.Infrastructure.Auth.Identity
{
    public class AuthService : BaseApplicationService, IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IServiceProvider serviceProvider, UserManager<AppUser> userManager, 
                IJwtProvider jwtProvider, ApplicationDbContext context, ILogger<AuthService> logger) 
                : base(serviceProvider, logger)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _context = context;
            _logger = logger;
        }
        

        // ===============================
        //   LOGIN
        // ===============================
        public async Task<AppResult<LoginResponseDTO>> LoginAsync(LoginRequestDTO request)
        {
            return await ExecuteAsync(request, async () =>
            {
                
                var user = await _userManager.FindByNameAsync(request.phone);

                if(user is null)
                    throw new DomainException(401, "Phone Number is Not Valid.");
                
                var passwordValid = await _userManager.CheckPasswordAsync(user, request.password);

                if(!passwordValid)
                    throw new DomainException(401, "Password Incorrect.");
                
                var roles = await _userManager.GetRolesAsync(user);

                if(roles == null || !roles.Any())
                    throw new AppException(404, "Roles Not Found.");

                var entityId = await GetEntityIdAsync(user.Id, roles.First());

                var accessToken = await _jwtProvider.CreateToken(user.Id, entityId, roles);

                var refreshToken = _jwtProvider.CreateRefreshToken();

                var refreshTokenEntity = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(30),
                    IsRevoked = false
                };

                await _context.RefreshTokens.AddAsync(refreshTokenEntity);
                await _context.SaveChangesAsync();

                var result = new LoginResponseDTO(user.Id, accessToken, refreshToken, user.PhoneNumber!, roles);

                return result;
            });
            
        }

        // ===============================
        //   Regsiter
        // ===============================
        public async Task<AppResult<RegisterResponseDTO>> RegisterAsync(RegisterRequestDTO request)
        {
            return await ExecuteAsync(request, async () =>
            {
                    
                var user = new AppUser(request.firstName, request.lastName, 
                                request.phone, request.email, request.gender, request.birthDate);
                
                _logger.LogInformation("Starting user creation...");

                var create = await _userManager.CreateAsync(user, request.password);

                _logger.LogInformation($"User {user.Id} created...");


                if(!create.Succeeded)
                {
                    _logger.LogWarning($"User {user.Id} not created...");

                    foreach (var error in create.Errors)
                    {
                        _logger.LogError("Code: {Code} | Description: {Description}",
                            error.Code,
                            error.Description);
                        throw new DomainException(400, error.Description);
                    }
                }
                
                await _userManager.AddToRoleAsync(user, request.role.ToString());


                var entityId = await CreateRoleEntityAsync(request, user, request.role);

                
                _logger.LogInformation("User add to role..");

                var roles = await _userManager.GetRolesAsync(user);

                var accessToken = await _jwtProvider.CreateToken(user.Id, entityId, roles);

                var refreshToken = _jwtProvider.CreateRefreshToken();

                var refreshTokenEntity = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(30),
                    IsRevoked = false
                };

                await _context.RefreshTokens.AddAsync(refreshTokenEntity);
                await _context.SaveChangesAsync();

                return new RegisterResponseDTO(user.Id, accessToken, refreshToken, roles);

            }, 201);
        }


        // ===============================
        //  UPDATE 
        // ===============================
        public async Task<AppResult> UpdateProfileAsync(string id, UpdateProfileDTO request)
        {
            return await ExecuteAsync(request, async () =>
            {
                if(string.IsNullOrWhiteSpace(id))
                    throw new AppException(404, "Identity Id Not Found.");

                var user = await _userManager.FindByIdAsync(id);

                if(user is null)
                    throw new AppException(404, "User Not Found.");

                user.Update(request.firstName, request.lastName, user.PhoneNumber!, request.email, request.gender, request.birthDate);

                await _context.SaveChangesAsync();

            });
        }


        public async Task<AppResult> DeleteProfileAsync(string id)
        {
            return await ExecuteAsync(async () =>
            {
                var user = await _userManager.FindByIdAsync(id);

                if(user is null)
                    throw new AppException(404, "User Not Found.");
                
                await _userManager.DeleteAsync(user);
            });
        }

        // ===============================
        //   RESET PASSWORD
        // ===============================

        public async Task<AppResult> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            return await ExecuteAsync(request, async () =>
            {
                
                // after otp
                var user = await _userManager.FindByNameAsync(request.phone);

                if(user is null)
                    throw new DomainException(404, "User Not Found.");
                    
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // var result = await _userManager.AddPasswordAsync(user, request.newPassword);
                var result = await _userManager.ResetPasswordAsync(user, token, request.newPassword);

                if(!result.Succeeded)
                    throw new DomainException(400, "Faild to Set The New Password.");

                
            }, 204);
            
        }



        public async Task<AppResult<string>> RenewToken(string refreshToken)
        {
            return await ExecuteAsync(refreshToken, async () =>
            {
                    
                var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
                
                if(token is null)
                    throw new AppException(404, "Token is Not Valid.");

                var user = await _userManager.FindByIdAsync(token.UserId);

                if(user is null)
                    throw new DomainException(401, "Phone Number is Not Valid.");
                
                
                var roles = await _userManager.GetRolesAsync(user);

                if(roles == null || !roles.Any())
                    throw new AppException(404, "Roles Not Found.");

                var entityId = await GetEntityIdAsync(user.Id, roles.First());

                var newToken = await _jwtProvider.CreateToken(user.Id, entityId, roles);

                return newToken;
            });
        }







        // =======================================
        //  HELPER: Add User To Its Role Entity
        // =======================================

        private async Task<Guid> CreateRoleEntityAsync(RegisterRequestDTO request, AppUser user, Roles role)
        {
            Guid userId = Guid.Empty;
            switch (role)
            {
                case Roles.Admin:
                    var admin = new AdminEntity(user.Id, user.FirstName, user.LastName, user.PhoneNumber!, request.city, request.address, null);
                    await _context.Admins.AddAsync(admin);
                    userId = admin.Id;
                    break;

                case Roles.Player:
                    var player = new PlayerEntity(user.Id, user.FirstName, user.LastName, user.PhoneNumber!, request.city, request.address, null);
                    await _context.Players.AddAsync(player);
                    userId = player.Id;
                    break;

                case Roles.Owner:
                    var owner = new OwnerEntity(user.Id, user.FirstName, user.LastName, user.PhoneNumber!, request.city, request.address, null);
                    await _context.Owners.AddAsync(owner);
                    userId = owner.Id;
                    break;
            }

            await _context.SaveChangesAsync();

            return userId;
        }


        // =======================================
        //  HELPER: Get User Role Entity
        // =======================================

        private async Task<Guid> GetEntityIdAsync(string identityId, string role)
        {
            return role switch
            {
                "Player" => await _context.Players
                    .Where(x => x.IdentityId == identityId)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync(),

                "Owner" => await _context.Owners
                    .Where(x => x.IdentityId == identityId)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync(),

                "Employee" => await _context.Employees
                    .Where(x => x.IdentityId == identityId)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync(),

                "Admin" => await _context.Admins
                    .Where(x => x.IdentityId == identityId)
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync(),

                _ => throw new DomainException(400, "Invalid role")
            };
        }
    }
}