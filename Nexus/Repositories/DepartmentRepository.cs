using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.Entities;
using Nexus.Interfaces;

namespace Nexus.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly NexusDBContext _context;

        public DepartmentRepository(NexusDBContext context)
        {
            _context = context;
        }

        // Get all departments for a specific hospital by HospitalId
        public async Task<IEnumerable<Department>> GetAllAsync(int hospitalId)
        {
            return await _context.Departments
                .Where(d => d.HospitalId == hospitalId) // Filter departments based on HospitalId
                .Include(d => d.Hospital) // Optionally include the Hospital entity
                .ToListAsync();
        }

        // Get a department by Id
        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        // Add a new department
        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        // Update an existing department
        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        // Delete a department by Id
        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }

}
