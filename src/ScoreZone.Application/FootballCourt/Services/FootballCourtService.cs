using Microsoft.Extensions.Logging;
using ScoreZone.Application.FootballCourt.DTOs;
using ScoreZone.Application.FootballCourt.Mappings;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Helpers;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.FootballCourt.Interfaces
{
    public class FootballCourtService : BaseApplicationService, IFootballCourtService
    {

        private readonly IFootballCourtRepository _repo;
        private readonly IFileService _fileService;
        private readonly ICurrentUser _currentUser;

        public FootballCourtService(IFootballCourtRepository repo, 
            IFileService fileService,
            ICurrentUser currentUser,
            IServiceProvider serviceProvider, 
            ILogger<FootballCourtService> logger)
        : base(serviceProvider, logger)
        {
            _repo = repo;
            _fileService = fileService;
            _currentUser = currentUser;
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


        
        public async Task<AppResult<PaginatedResultDto<FootballCourtDetailsDto>>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await ExecuteAsync(async () =>
            {
                var skip = PaginationHelper.Skip(pageNumber, pageSize);

                var courts = await _repo.GetAllAsync(skip, pageSize);

                return new PaginatedResultDto<FootballCourtDetailsDto>(courts.items, courts.count, pageNumber, pageSize);
                
            });
        }

        
        public async Task<AppResult<FootballCourtDetailsDto>> GetByIdAsync(Guid id)
        {
            return await ExecuteAsync(id, async () =>
            {
                var court = await _repo.GetByIdAsync(id); 

                if(court is null)
                    throw new AppException(404, "Court Not Found.");
                
                if(_currentUser.role == "Player" && court.Status != CourtStatus.Active)
                    throw new AppException(403, "Court Not Available.");
                
                return court.ToDto();

            });
        }


        public async Task<AppResult<PaginatedResultDto<FootballCourtDetailsDto>>> BrowseNearbyCourtsAsync(LocationCoordsRequest request, int pageNumber, int pageSize)
        {
            return await ExecuteAsync(request, async () =>
            {
                var skip = PaginationHelper.Skip(pageNumber, pageSize);

                var allCourts = await _repo.GetAllActiveAsync(request.city);

                var nearestCourts = allCourts.items
                    .Select(x => 
                    { 
                        var dto = x.ToDto(); return dto with
                        {
                            distance = GeoHelper.CalculateDistance(request.locationLat, 
                                    request.locationLng, x.LocationLat, x.LocationLng)
                        };
                })
                .OrderByDescending(x => x.distance)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

                var count = allCourts.count;

                return new PaginatedResultDto<FootballCourtDetailsDto>(nearestCourts, count, pageNumber, pageSize);

            });
        }

        public async Task<AppResult> ChangeStatusAsync(Guid id, CourtStatus status)
        {
            return await ExecuteAsync(id, async () =>
            {
                var court = await _repo.GetByIdAsync(id);

                if(court is null)
                    throw new AppException(404, "Court Not Found.");
                
                switch (status)
                {
                    case CourtStatus.Pending:
                        court.Pend();
                        break;

                    case CourtStatus.Active:
                        court.Accept();
                        break;
                    
                    case CourtStatus.Rejected:
                        court.Reject();
                        break;
                    
                    case CourtStatus.Blocked:
                        court.Block();
                        break;

                    default:
                        court.Pend();
                        break;
                }

                await _repo.SaveChangesAsync();
            });
        }
        
    }
    
}