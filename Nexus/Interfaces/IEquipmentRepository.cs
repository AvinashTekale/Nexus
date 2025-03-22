using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetAllAsync(int hospitalId);
        Task<Equipment> GetByIdAsync(string serialNumber);
        Task AddAsync(Equipment equipment);
        Task UpdateAsync(Equipment equipment);
        Task DeleteAsync(string serialNumber);
    }
}
