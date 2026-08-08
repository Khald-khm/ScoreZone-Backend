using Microsoft.Extensions.Logging;
using ScoreZone.Application.Auth;
using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Facility.Mappings;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Application.User.Player.DTOs;
using ScoreZone.Application.User.Player.Mappings;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.User.Player.Interfaces
{
    public class PlayerService : BaseApplicationService, IPlayerService
    {

        private readonly IPlayerRepository _repo;
        private readonly IAuthService _authService;
        private readonly IFileService _fileService;
        private readonly ICurrentUser _currentUser;

        public PlayerService(
                IPlayerRepository repo, 
                IAuthService authService,
                IFileService fileService,
                ICurrentUser currentUser,
                IServiceProvider serviceProvider, 
                ILogger<PlayerService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _authService = authService;
            _fileService = fileService;
            _currentUser = currentUser;
        }

        public async Task<AppResult> AddAsync(AddPlayerRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var player = request.ToEntity();

                if(request.profileImage is not null)
                    player.ProfileImage = await _fileService.UploadFileAsync(request.profileImage);

                await _repo.AddAsync(player);
                await _repo.SaveChangesAsync();
            });
        }

        public async Task<AppResult> UpdateAsync(Guid? id, UpdatePlayerRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var playerId = id ?? _currentUser.userId.Value;

                var player = await _repo.GetByIdAsync(playerId);

                if(player is null)
                    throw new AppException(404, "Player Not Found.");
                
                player.Update(request.firstName, request.lastName, player.PhoneNumber, request.city, request.address);

                // UPDATE OR DELETE PROFILE IMAGE
                if(string.IsNullOrWhiteSpace(request.profileImageUrl))
                {
                    if(player.ProfileImage is not null)
                    {
                        await _fileService.DeleteFileAsync(player.ProfileImage);
                        player.ProfileImage = null;
                    }   

                    if(request.profileImage is not null)
                        player.ProfileImage = await _fileService.UploadFileAsync(request.profileImage);
                }

                await _authService.UpdateProfileAsync(player.IdentityId, request.ToAuth());

                await _repo.SaveChangesAsync();

            });
        }

        public async Task<AppResult> DeleteAsync(Guid? id)
        {
            return await ExecuteAsync(async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var playerId = id ?? _currentUser.userId.Value;

                var player = await _repo.GetByIdAsync(playerId);
                
                if(player is null)
                    throw new AppException(404, "Player Not Found.");
                
                if(player.ProfileImage is not null)
                    await _fileService.DeleteFileAsync(player.ProfileImage);
                
                await _repo.DeleteAsync(playerId);
                
            });
        }

        public async Task<AppResult<PlayerDetailsResponse>> GetByIdAsync(Guid? id)
        {
            return await ExecuteAsync(id, async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var playerId = id ?? _currentUser.userId.Value;

                if(id is not null && _currentUser.role != "Admin")
                    throw new AppException(403, "You Are Not Allowed.");
                
                var player = await _repo.GetByIdAsync(playerId);

                if(player is null)
                    throw new AppException(404, "Player Not Found.");

                return player.ToDto();
            });
        }
        
    }
    
}