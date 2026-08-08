using Microsoft.EntityFrameworkCore;
using ScoreZone.Application.User.Employee.DTOs;
using ScoreZone.Application.User.Employee.Interfaces;
using ScoreZone.Domain.User.Employee;
using ScoreZone.Infrastructure.Data;

namespace ScoreZone.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(EmployeeEntity employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Employees.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<EmployeeEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> OwnsEmployee(Guid ownerId, Guid employeeId)
        {
            return await _context.Employees.AnyAsync(x => x.Id == employeeId && x.OwnerId == ownerId);
        }

        public async Task<List<Guid>> MyFootballCourts(Guid employeeId)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == employeeId)
                .SelectMany(x => x.FootballCourts.Select(x => x.Id))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<EmployeeEntity>> MyEmployees(Guid ownerId)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(x => x.OwnerId == ownerId)
                .ToListAsync();
        }
    }
}