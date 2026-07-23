using Microsoft.Extensions.Logging;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Helpers;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Application.User.Owner.DTOs;
using ScoreZone.Application.User.Owner.Interfaces;
using ScoreZone.Application.User.Owner.Mappings;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.User.Owner.Interfaces
{
    public class OwnerService : BaseApplicationService, IOwnerService
    {

        private readonly IOwnerRepository _repo;
        private readonly IFileService _fileService;

        public OwnerService(IOwnerRepository repo, 
                IFileService fileService,
                IServiceProvider serviceProvider, 
                ILogger<OwnerService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _fileService = fileService;
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

        public async Task<AppResult<OwnerDetailsResponse>> GetByIdAsync(Guid id)
        {
            return await ExecuteAsync(id, async () =>
            {
               var owner = await _repo.GetByIdAsync(id); 

               if(owner is null)
                throw new AppException(404, "Owner Not Found.");

               return owner.ToDto();
            });
        }
        
    }
    
}