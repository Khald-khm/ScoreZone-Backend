using Microsoft.Extensions.Logging;
using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Facility.Mappings;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.Facility.Services
{
    public class FacilityService : BaseApplicationService, IFacilityService
    {

        private readonly IFacilityRepository _repo;
        private readonly IFileService _fileService;

        public FacilityService(
                    IFacilityRepository repo, 
                    IFileService fileService,
                    IServiceProvider serviceProvider, 
                    ILogger<FacilityService> logger) 
        : base(serviceProvider, logger)
        {
            _repo = repo;
            _fileService = fileService;
        }

        public async Task<AppResult> AddAsync(AddFacilityRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var facility = request.ToEntity();

                facility.ProfileImage = null;
                if(request.profileImage is not null)
                {
                    var imageUrl = await _fileService.UploadFileAsync(request.profileImage);
                    facility.ProfileImage = imageUrl;
                }

                await _repo.AddAsync(facility);
                await _repo.SaveChangesAsync();
            });
        }

        public async Task<AppResult<FacilityDetailsDto>> GetByIdAsync(Guid id)
        {
            return await ExecuteAsync(id, async () =>
            {
                var facility = await _repo.GetByIdAsync(id);

                if(facility is null)
                    throw new AppException(404, "Facility Not Found.");

                return facility.ToDto();
            });
        }
        
    }
    
}