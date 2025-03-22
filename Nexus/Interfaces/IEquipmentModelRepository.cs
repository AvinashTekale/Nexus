using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IEquipmentModelRepository
    {
        Task<IEnumerable<EquipmentModel>> GetAllAsync(int hospitalId);
        Task<EquipmentModel> GetByIdAsync(int id);
        Task<IEnumerable<EquipmentModel>> GetByPartnerIdAsync(int partnerId);
        Task AddAsync(EquipmentModel modelName);
        Task UpdateAsync(EquipmentModel modelName);
        Task DeleteAsync(int id);
    }
}
