using Nexus.DTOs;
using Nexus.Entities;

namespace Nexus.Interfaces
{
    public interface IContactPersonRepository
    {
        Task<IEnumerable<ContactPersonDTO>> GetAllAsync();
        Task<ContactPersonDTO> GetByIdAsync(int id);
        Task AddAsync(ContactPersonDTO contactPersonDto);
        Task UpdateAsync(ContactPersonDTO contactPersonDto);
        Task DeleteAsync(int id);
    }
}
