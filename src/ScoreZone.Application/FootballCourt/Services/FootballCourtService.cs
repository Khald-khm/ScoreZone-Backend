using Microsoft.Extensions.Logging;
using ScoreZone.Application.FootballCourt.DTOs;
using ScoreZone.Application.FootballCourt.Mappings;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;

namespace ScoreZone.Application.FootballCourt.Interfaces
{
    public class FootballCourtService : BaseApplicationService, IFootballCourtService
    {

        private readonly IFootballCourtRepository _repo;
        private readonly IFileService _fileService;

        public FootballCourtService(IFootballCourtRepository repo, 
            IFileService fileService,
            IServiceProvider serviceProvider, 
            ILogger<FootballCourtService> logger)
        : base(serviceProvider, logger)
        {
            _repo = repo;
            _fileService = fileService;
        }

        public async Task<AppResult> AddAsync(AddFootballCourtRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var court = request.ToEntity();

                court.ProfileImage = null;
                if(request.profileImage is not null)
                {
                    var imageUrl = await _fileService.UploadFileAsync(request.profileImage);
                    court.ProfileImage = imageUrl;
                }

                await _repo.AddAsync(court);
                await _repo.SaveChangesAsync();
            });
        }

        
        public async Task<AppResult> GetByIdAsync(Guid id)
        {
            // TODO: MAKE DTO TO RETURN IT IN THE RESULT
            return await ExecuteAsync(id, async () =>
            {
               await _repo.GetByIdAsync(id); 
            });
        }


        public async Task<AppResult> BrowseNearbyCourtsAsync(LocationCoordsRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                // TODO: MAKE DTO, PAGINATE THE RESULT
                //       MAKE THE ALGORITHM TO RETRIEVE THE RESULT
            });
        }
        
    }
    
}