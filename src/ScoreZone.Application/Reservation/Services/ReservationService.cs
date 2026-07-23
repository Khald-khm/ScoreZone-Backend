using Microsoft.Extensions.Logging;
using ScoreZone.Application.FootballCourt.Interfaces;
using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.Reservation.Interfaces;
using ScoreZone.Application.Reservation.Mappings;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Helpers;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.Reservation.Services
{
    public class ReservationService : BaseApplicationService, IReservationService
    {

        private readonly IReservationRepository _repo;
        private readonly IFootballCourtRepository _courtRepo;

        private readonly ICurrentUser _currentUser;

        public ReservationService(IReservationRepository repo, 
            IFootballCourtRepository courtRepo,
            ICurrentUser currentUser,
            IServiceProvider serviceProvider, 
            ILogger<ReservationService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _courtRepo = courtRepo;
            _currentUser = currentUser;
        }

        public async Task<AppResult> AddAsync(AddUpdateReservationRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var reservation = request.ToEntity();


                var court = await GetCourt(request.courtId);

                if(court!.Status != CourtStatus.Active)
                    throw new AppException(403, "Court is Not Active.");
                    

                var reservedSlots = await _repo.GetAllByDayAsync(request.courtId, request.reservationDate);
                if(reservedSlots.Any(x => x.TimeSlotNum == request.timeSlotNum))
                    throw new AppException(403, "Time Slot Not Available.");


                await _repo.AddAsync(reservation);

                await _repo.SaveChangesAsync();
            });
        }
        
        
        public async Task<AppResult> UpdateAsync(Guid id, AddUpdateReservationRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var court = await GetCourt(request.courtId);

                if(court!.Status != CourtStatus.Active)
                    throw new AppException(403, "Court is Not Active.");
                    

                var reservedSlots = await _repo.GetAllByDayAsync(request.courtId, request.reservationDate);
                if(reservedSlots.Any(x => x.TimeSlotNum == request.timeSlotNum))
                    throw new AppException(403, "Time Slot Not Available.");


                var reservation = await _repo.GetByIdAsync(id);

                if( reservation is null)
                    throw new AppException(404, "Reservation Not Found.");

                reservation.Update(request.ToEntity());

                await _repo.SaveChangesAsync();
            });
        }


        public async Task<AppResult<ReservationDetails>> GetDetailsByIdAsync(Guid id)
        {
            return await ExecuteAsync(id, async () =>
            {
                if(!_currentUser.userId.HasValue)
                    throw new AppException(401, "User Id is Missing From Token.");

                Guid userId = Guid.Parse(_currentUser.userId.Value.ToString());

                var reservation = await _repo.GetDetailsByIdAsync(id);
                
                if(reservation is null)
                    throw new AppException(404, "Reservation Not Found.");
                
                if(nameof(Roles.Player) == _currentUser.role!.ToString() && reservation.playerId != userId)
                    throw new AppException(403, "You are Not Allowed.");
                
                return reservation;
            });
        }

        public async Task<AppResult<IReadOnlyList<ReservedSlots>>> GetReservedSlotsAsync(ViewAvailableSlotsRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var slots = await _repo.GetAllByDayAsync(request.courtId, request.date);
                
                IReadOnlyList<ReservedSlots> result = slots.Select(x => x.ToDto()).ToList();

                return result;
            });
        }


        public async Task<AppResult<PaginatedResultDto<MyReservation>>> GetMyReservationsAsync(int pageNumber, int pageSize)
        {
            return await ExecuteAsync( async () =>
            {
                PaginationHelper.Normalize(ref pageNumber, ref pageSize);
                var skip = PaginationHelper.Skip(pageNumber, pageSize);

                var currentUserId = _currentUser.userId?.ToString();

                if(string.IsNullOrWhiteSpace(currentUserId))
                    throw new AppException(403, "You Are Not Authenticated.");
                
                if(!Guid.TryParse(currentUserId, out Guid userId))
                    userId = Guid.Empty;

                var result = await _repo.GetMyReservationsAsync(userId, skip, pageSize);

                return new PaginatedResultDto<MyReservation> (result.items, result.count, pageNumber, pageSize);
            });
        }


        public async Task<AppResult> PayDepositeAsync(Guid id, PayDepositeRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var reservation = await _repo.GetByIdAsync(id);

                if(reservation is null)
                    throw new AppException(404, "Reservation Not Found.");

                reservation.PayDeposite(request.depositeAmount);

                await _repo.SaveChangesAsync();
            });
        }



        // ==============================
        // HELPER: GET COURT
        // ==============================
        private async Task<FootballCourtEntity?> GetCourt(Guid id)
        {
            var court = await _courtRepo.GetByIdAsync(id);

            if(court is null)
                throw new AppException(404, "Court Not Found.");
            
            return court;
        }
        
    }
    
}