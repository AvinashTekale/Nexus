using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IPartnerRepository
    {
        Task<IEnumerable<Partner>> GetAllAsync();
        Task<Partner?> GetByIdAsync(int id);
        Task<IEnumerable<Partner>> GetByHospitalIdAsync(int hospitalId);
        Task AddAsync(Partner partner);
        Task UpdateAsync(Partner partner);
        Task DeleteAsync(int id);
    }
}
