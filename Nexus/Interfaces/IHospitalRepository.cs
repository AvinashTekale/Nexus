using Nexus.DTOs;
using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAllAsync();
        Task<Hospital> GetByIdAsync(int id);
        Task<Hospital> CreateAsync(HospitalDTO dto);
        Task<Hospital> UpdateAsync(int id, HospitalDTO dto);
        Task<bool> DeleteAsync(int id);
        IEnumerable<State> GetStates();
        IEnumerable<City> GetCitiesByState(int stateId);
    }
}
