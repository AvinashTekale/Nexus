using Nexus.DTOs;
using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IBMEAccountRepository
    {
        Task<IEnumerable<BMEAccount>> GetAllAsync(int HospitalId);
        Task<BMEAccount> GetByIdAsync(int id);
        Task<BMEAccount> CreateAsync(BMEAccountDTO dto);
        Task<BMEAccount> UpdateAsync(int id, BMEAccountDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
