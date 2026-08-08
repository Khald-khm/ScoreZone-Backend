using Microsoft.Extensions.Logging;
using ScoreZone.Application.Auth;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Helpers;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Application.User.Owner.DTOs;
using ScoreZone.Application.User.Owner.Mappings;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.User.Owner.Interfaces
{
    public class OwnerService : BaseApplicationService, IOwnerService
    {

        private readonly IOwnerRepository _repo;
        private readonly IFileService _fileService;
        private readonly IAuthService _authService;
        private readonly ICurrentUser _currentUser;

        public OwnerService(IOwnerRepository repo, 
                IFileService fileService,
                IAuthService authService,
                ICurrentUser currentUser,
                IServiceProvider serviceProvider, 
                ILogger<OwnerService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _fileService = fileService;
            _authService = authService;
            _currentUser = currentUser;
        }

        // public async Task<AppResult> AddAsync(AddOwnerRequest request)
        // {
        //     return await ExecuteAsync(request, async () =>
        //     {
        //         var owner = request.ToEntity();

        //         owner.ProfileImage = null;
        //         if(request.profileImage is not null)
        //         {
        //             var imageUrl = await _fileService.UploadFileAsync(request.profileImage);
        //             owner.ProfileImage = imageUrl;
        //         }

        //         await _repo.AddAsync(owner);
        //         await _repo.SaveChangesAsync();
        //     });
        // }

        public async Task<AppResult> UpdateAsync(Guid? id, UpdateOwnerRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var ownerId = id ?? _currentUser.userId.Value;
                
                var owner = await _repo.GetByIdAsync(ownerId);

                if(owner is null)
                    throw new AppException(404, "Owner Not Found.");
                
                owner.Update(request.firstName, request.lastName, owner.PhoneNumber, request.city, request.address);

                // UPDATE OR DELETE PROFILE IMAGE
                if(string.IsNullOrWhiteSpace(request.profileImageUrl))
                {
                    if(owner.ProfileImage is not null)
                    {
                        await _fileService.DeleteFileAsync(owner.ProfileImage);
                        owner.ProfileImage = null;
                    }

                    if(request.profileImage is not null)
                        owner.ProfileImage = await _fileService.UploadFileAsync(request.profileImage);
                }
                
                await _authService.UpdateProfileAsync(owner.IdentityId, request.ToAuth());

                await _repo.SaveChangesAsync();

            });
        }

        public async Task<AppResult> DeleteAsync(Guid? id)
        {
            return await ExecuteAsync(async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var ownerId = id ?? _currentUser.userId.Value;

                var owner = await _repo.GetByIdAsync(ownerId);
                
                if(owner is null)
                    throw new AppException(404, "Owner Not Found.");
                
                if(owner.ProfileImage is not null)
                    await _fileService.DeleteFileAsync(owner.ProfileImage);
                
                await _repo.DeleteAsync(ownerId);
                
            });
        }

        public async Task<AppResult<PaginatedResultDto<OwnerDetailsResponse>>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await ExecuteAsync(async () =>
            {
                var skip = PaginationHelper.Skip(pageNumber, pageSize);

                var owners = await _repo.GetAllAsync(skip, pageSize);

                return new PaginatedResultDto<OwnerDetailsResponse>(owners.items, owners.count, pageNumber, pageSize);
            });
        }

        public async Task<AppResult<IReadOnlyCollection<OwnerShortResponse>>> GetAllShortAsync()
        {
            return await ExecuteAsync(async () =>
            {
                return await _repo.GetAllShortAsync();
            });
        }

        public async Task<AppResult<OwnerDetailsResponse>> GetByIdAsync(Guid? id)
        {
            return await ExecuteAsync(async () =>
            {
                if(_currentUser.userId is null)
                    throw new AppException(404, "User Not Found.");

                var ownerId = id ?? _currentUser.userId.Value;

                if(id is not null && _currentUser.role != "Admin")
                    throw new AppException(403, "You Are Not Allowed.");
               
                var owner = await _repo.GetByIdAsync(ownerId);

                if(owner is null)
                    throw new AppException(404, "Owner Not Found.");

                return owner.ToDto();
            });
        }

        
    }
    
}