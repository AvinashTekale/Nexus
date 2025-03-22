using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.Entities;
using Nexus.Interfaces;

namespace Nexus.Repositories
{
    public class EquipmentModelRepository : IEquipmentModelRepository
    {
        private readonly NexusDBContext _context;

        public EquipmentModelRepository(NexusDBContext context)
        {
            _context = context;
        }

        // Get all ModelName records
        public async Task<IEnumerable<EquipmentModel>> GetAllAsync(int hospitalId)
        {
            return await _context.EquipmentModels.Where(r => r.Partner.HospitalId == hospitalId).Include(m => m.Partner).ToListAsync();
        }

        // Get a ModelName by ID
        public async Task<EquipmentModel> GetByIdAsync(int id)
        {
            return await _context.EquipmentModels.Include(m => m.Partner).FirstOrDefaultAsync(m => m.Id == id);
        }

        // Get ModelName records by PartnerId
        public async Task<IEnumerable<EquipmentModel>> GetByPartnerIdAsync(int partnerId)
        {
            return await _context.EquipmentModels.Where(m => m.PartnerId == partnerId).ToListAsync();
        }

        // Add a new ModelName
        public async Task AddAsync(EquipmentModel modelName)
        {
            await _context.EquipmentModels.AddAsync(modelName);
            await _context.SaveChangesAsync();
        }

        // Update an existing ModelName
        public async Task UpdateAsync(EquipmentModel modelName)
        {
            _context.EquipmentModels.Update(modelName);
            await _context.SaveChangesAsync();
        }

        // Delete a ModelName by ID
        public async Task DeleteAsync(int id)
        {
            var modelName = await GetByIdAsync(id);
            if (modelName != null)
            {
                _context.EquipmentModels.Remove(modelName);
                await _context.SaveChangesAsync();
            }
        }
    }
}
