using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync(int hospitalId); // Get all departments based on HospitalId
        Task<Department> GetByIdAsync(int id); // Get department by Id
        Task AddAsync(Department department); // Add a new department
        Task UpdateAsync(Department department); // Update an existing department
        Task DeleteAsync(int id); // Delete a department by Id
    }
}
