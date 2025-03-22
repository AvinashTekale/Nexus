using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.Entities;
using Nexus.Interfaces;

namespace Nexus.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly NexusDBContext _context;

        public PartnerRepository(NexusDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Partner>> GetAllAsync()
        {
            return await _context.Partners
                .Include(p => p.Hospital) // Include navigation property
                .ToListAsync();
        }

        public async Task<Partner?> GetByIdAsync(int id)
        {
            return await _context.Partners
                .Include(p => p.Hospital) // Include navigation property
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Partner>> GetByHospitalIdAsync(int hospitalId)
        {
            return await _context.Partners
                .Where(p => p.HospitalId == hospitalId)
                .Include(p => p.Hospital) // Include navigation property
                .ToListAsync();
        }

        public async Task AddAsync(Partner partner)
        {
            await _context.Partners.AddAsync(partner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Partner partner)
        {
            _context.Partners.Update(partner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var partner = await GetByIdAsync(id);
            if (partner != null)
            {
                _context.Partners.Remove(partner);
                await _context.SaveChangesAsync();
            }
        }
    }

}
