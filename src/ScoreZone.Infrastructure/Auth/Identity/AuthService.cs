using ScoreZone.Application.Auth;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace ScoreZone.Infrastructure.Auth.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly ApplicationDbContext _context;

        public AuthService(UserManager<AppUser> userManager, IJwtProvider jwtProvider, ApplicationDbContext context)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _context = context;
        }
        

        // ===============================
        //   LOGIN
        // ===============================
        public async Task<AppResult<LoginResponseDTO>> LoginAsync(LoginRequestDTO request)
        {
            var user = await _userManager.FindByNameAsync(request.username);

            if(user is null)
                return Result.DomainError(401, "Username is Not Valid.");
            
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.password);

            if(!passwordValid)
                return Result.DomainError(401, "Password Incorrect.");
            
            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = await _jwtProvider.CreateToken(user.Id, user.FirstName, user.LastName, user.UserName!, roles);

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

            var result = new LoginResponseDTO(user.Id, accessToken, refreshToken, user.UserName!, roles);

            return Result.Success(result, 200, "Login Successful.");
            
        }

        // ===============================
        //   Regsiter
        // ===============================
        public async Task<AppResult<RegisterResponseDTO>> RegisterAsync(RegisterRequestDTO request)
        {
            var user = new AppUser(request.firstName, request.lastName, request.username, 
                            request.phone, request.email, request.gender, request.birthDate);
            
            var create = await _userManager.CreateAsync(user, request.password);

            await _userManager.AddToRoleAsync(user, request.role);

            // TODO: You have to add it to the specific user table (employee, manager, admin....) each role has a table
            // YOU CAN USE FACTORY METHOD PATTERN TO HANDLE IT

            if(!create.Succeeded)
                return Result.DomainError(400, "Cannot Create User.");
            
            await _context.SaveChangesAsync();

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = await _jwtProvider.CreateToken(user.Id, user.FirstName, user.LastName, user.UserName!, roles);

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

            var result = new RegisterResponseDTO(user.Id, accessToken, refreshToken, roles);
            

            return Result.Success(result, 201, "User Created Successfuly.");

        }


        // ===============================
        //   RESET PASSWORD
        // ===============================

        public async Task<AppResult> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            
            // after otp
            var user = await _userManager.FindByNameAsync(request.username);

            if(user is null)
                return Result.DomainError(404, "User Not Found.");
            
            var removePassword = await _userManager.RemovePasswordAsync(user);

            if(!removePassword.Succeeded)
                return Result.DomainError(400, "Faild to Remove Old Password.");

            var result = await _userManager.AddPasswordAsync(user, request.newPassword);

            if(!result.Succeeded)
                return Result.DomainError(400, "Faild to Set The New Password.");

            return Result.Success(204, "Password Updated Successfuly.");
        }
    }
}