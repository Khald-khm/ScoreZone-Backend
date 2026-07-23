using Microsoft.Extensions.Logging;
using ScoreZone.Application.Facility.DTOs;
using ScoreZone.Application.Facility.Interfaces;
using ScoreZone.Application.Facility.Mappings;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;

namespace ScoreZone.Application.User.Player.Interfaces
{
    public class PlayerService : BaseApplicationService
    {

        private readonly IFacilityRepository _repo;
        private readonly IFileService _fileService;

        public PlayerService(IFacilityRepository repo, 
                IFileService fileService,
                IServiceProvider serviceProvider, 
                ILogger<PlayerService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _fileService = fileService;
        }

        // public async Task<AppResult> AddAsync(AddFacilityRequest request)
        // {
        //     return await ExecuteAsync(request, async () =>
        //     {
        //         var player = request.ToEntity();

        //         _fileService.UploadFileAsync(player.ProfileImage);

        //         await _repo.AddAsync(player);
        //         await _repo.SaveChangesAsync();
        //     });
        // }

        // public async Task<AppResult> GetByIdAsync(Guid id)
        // {
        //     return await ExecuteAsync(id, async () =>
        //     {
        //        await _repo.GetByIdAsync(id); 
        //     });
        // }
        
    }
    
}