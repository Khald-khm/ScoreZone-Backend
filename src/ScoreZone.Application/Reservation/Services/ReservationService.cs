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
using ScoreZone.Application.User.Employee.Interfaces;
using ScoreZone.Application.User.Owner.Interfaces;
using ScoreZone.Domain.FootballCourt;
using ScoreZone.Domain.Reservation.Enums;
using ScoreZone.Domain.Shared.Enum;
using ScoreZone.Domain.Shared.Exceptions;

namespace ScoreZone.Application.Reservation.Services
{
    public class ReservationService : BaseApplicationService, IReservationService
    {

        private readonly IReservationRepository _repo;
        private readonly IFootballCourtRepository _courtRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IEmployeeRepository _employeeRepo;

        private readonly ICurrentUser _currentUser;

        public ReservationService(IReservationRepository repo, 
            IFootballCourtRepository courtRepo,
            IOwnerRepository ownerRepo,
            IEmployeeRepository employeeRepo,
            ICurrentUser currentUser,
            IServiceProvider serviceProvider, 
            ILogger<ReservationService> logger) 
        : base(serviceProvider, logger) 
        {
            _repo = repo;
            _courtRepo = courtRepo;
            _ownerRepo = ownerRepo;
            _employeeRepo = employeeRepo;
            _currentUser = currentUser;
        }

        public async Task<AppResult> AddAsync(AddReservationRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {

                Guid? playerId = _currentUser.userId ?? request.playerId;

                if(playerId is null)
                    throw new AppException(404, "Player Id is Required.");

                var court = await GetCourt(request.courtId);

                if(court is null)
                    throw new AppException(404, "Football Court Not Found.");

                if(court!.Status != CourtStatus.Active)
                    throw new AppException(403, "Court is Not Active.");

                var reservedSlots = await _repo.GetAllByDayAsync(request.courtId, request.reservationDate);

                if(reservedSlots.Any(x => x.TimeSlotNum == request.timeSlotNum))
                    throw new AppException(403, "Time Slot Not Available.");
                    

                var reservation = request.ToEntity((Guid) playerId, court.PricePerMatch);

                await _repo.AddAsync(reservation);

                await _repo.SaveChangesAsync();
            });
        }
        
        
        public async Task<AppResult> UpdateAsync(Guid id, UpdateReservationRequest request)
        {
            return await ExecuteAsync(request, async () =>
            {
                var court = await GetCourt(request.courtId);

                if(court is null)
                    throw new AppException(404, "Football Court Not Found.");

                if(court!.Status != CourtStatus.Active)
                    throw new AppException(403, "Court is Not Active.");
                    

                var reservedSlots = await _repo.GetAllByDayAsync(request.courtId, request.reservationDate);
                if(reservedSlots.Any(x => x.TimeSlotNum == request.timeSlotNum))
                    throw new AppException(403, "Time Slot Not Available.");


                var reservation = await _repo.GetByIdAsync(id);

                if( reservation is null)
                    throw new AppException(404, "Reservation Not Found.");

                reservation.Update(request.ToEntity(reservation.PlayerId, court.PricePerMatch));

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



        public async Task<AppResult> DailyReservationsAsync(DateOnly date)
        {
            return await ExecuteAsync(date, async () =>
            {
                List<Guid> courtIds;

                if(!_currentUser.userId.HasValue)
                    throw new AppException(404, "User Not Found.");
                
                var userId = _currentUser.userId.Value;

                if(_currentUser.role == "Owner")
                    courtIds = await _ownerRepo.MyFootballCourts(userId);
                else
                    courtIds = await _employeeRepo.MyFootballCourts(userId);

                var reservations = await _repo.DailyReservations(date, courtIds);

                // return reservations;
            });
        }


        public async Task<AppResult> CheckInAsync(Guid reservationId, Guid playerId, int? completePayment)
        {
            return await ExecuteAsync(reservationId, async () =>
            {
                var reservation = await _repo.GetByIdAsync(reservationId);

                if(reservation is null)
                    throw new AppException(404, "Reservation Not Found.");

                if(reservation.PlayerId != playerId)
                    throw new AppException(409, "Reservation Does Not Belong To This Player.");
                
                if(reservation.Status != ReservationStatus.Paid || reservation.Status != ReservationStatus.Canceled)
                {
                    if(completePayment is null || completePayment <= 0)
                        throw new AppException(403, "Payment Amount Must Be Greater Than 0.");

                    reservation.CompletePayment((int)completePayment);
                }
                
                reservation.CheckIn();
                
            });
        }

        public async Task<AppResult<IReadOnlyList<SearchReservationDetails>>> Search(string searchWord)
        {
            return await ExecuteAsync(searchWord, async () =>
            {
                string word = searchWord.Replace(" ", string.Empty);
                
                return await _repo.Search(word); 
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