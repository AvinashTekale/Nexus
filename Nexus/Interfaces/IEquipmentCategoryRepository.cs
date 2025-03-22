using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IEquipmentCategoryRepository
    {
        Task<IEnumerable<EquipmentCategory>> GetAllAsync(int HospitalId);
        Task<EquipmentCategory> GetByIdAsync(int id);
        Task AddAsync(EquipmentCategory equipmentCategory);
        Task UpdateAsync(EquipmentCategory equipmentCategory);
        Task DeleteAsync(int id);
    }
}
