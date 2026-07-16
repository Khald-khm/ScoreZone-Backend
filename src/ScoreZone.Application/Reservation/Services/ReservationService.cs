using Microsoft.Extensions.Logging;
using ScoreZone.Application.Reservation.DTOs;
using ScoreZone.Application.Reservation.Interfaces;
using ScoreZone.Application.Reservation.Mappings;
using ScoreZone.Application.Shared.DTOs;
using ScoreZone.Application.Shared.Helpers;
using ScoreZone.Application.Shared.Interfaces;
using ScoreZone.Application.Shared.Results;
using ScoreZone.Application.Shared.Services;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.Reservation.Services
{
    public class ReservationService : BaseApplicationService, IReservationService
    {

        private readonly IReservationRepository _repo;

        private readonly ICurrentUser _currentUser;

        public ReservationService(IReservationRepository repo, 
            ICurrentUser currentUser,
            IServiceProvider serviceProvider, 
            ILogger<ReservationService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _currentUser = currentUser;
        }

        public async Task<AppResult> AddAsync(AddReservationRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var reservation = request.ToEntity();

                await _repo.AddAsync(reservation);

                await _repo.SaveChangesAsync();
            });
        }

        public async Task<AppResult<ReservationDetails>> GetDetailsByIdAsync(Guid id)
        {
            return await ExecuteAsync(id, async () =>
            {
                var reservation = await _repo.GetDetailsByIdAsync(id);
                
                if(reservation is null)
                    throw new AppException(404, "Reservation Not Found.");
                
                return reservation;
            });
        }

        public async Task<AppResult<IReadOnlyList<ReservedSlots>>> ViewReservedSlotsAsync(ViewAvailableSlotsRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var slots = await _repo.GetAllByDayAsync(request.courtId, request.date);
                
                IReadOnlyList<ReservedSlots> result = slots.Select(x => x.ToDto()).ToList();

                return result;
            });
        }


        public async Task<AppResult<PaginatedResultDto<MyReservation>>> GetMyReservationsAsync(Guid playerId, int pageNumber, int pageSize)
        {
            return await ExecuteAsync(playerId, async () =>
            {
                PaginationHelper.Normalize(ref pageNumber, ref pageSize);
                var skip = PaginationHelper.Skip(pageNumber, pageSize);

                var result = await _repo.GetMyReservationsAsync(playerId, skip, pageSize);

                return new PaginatedResultDto<MyReservation> (result.items, result.count, pageNumber, pageSize);
            });
        }


        public async Task<AppResult> PayDepositeAsync(PayDepositeRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var reservation = await _repo.GetByIdAsync(request.reservationId);

                if(reservation is null)
                    throw new AppException(404, "Reservation Not Found.");

                reservation.PayDeposite(request.depositeAmount);

                await _repo.SaveChangesAsync();
            });
        }
        
    }
    
}