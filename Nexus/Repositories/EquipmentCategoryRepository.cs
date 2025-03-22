using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.Entities;
using Nexus.Interfaces;

namespace Nexus.Repositories
{
    public class EquipmentCategoryRepository : IEquipmentCategoryRepository
    {
        private readonly NexusDBContext _context;

        public EquipmentCategoryRepository(NexusDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EquipmentCategory>> GetAllAsync(int hospitalId)
        {
            return await _context.EquipmentCategories
                .Where(ec => ec.HospitalId == hospitalId)
                .ToListAsync();
        }

        public async Task<EquipmentCategory> GetByIdAsync(int id)
        {
            return await _context.EquipmentCategories.FindAsync(id);
        }

        public async Task AddAsync(EquipmentCategory equipmentCategory)
        {
            await _context.EquipmentCategories.AddAsync(equipmentCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EquipmentCategory equipmentCategory)
        {
            _context.EquipmentCategories.Update(equipmentCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var equipmentCategory = await _context.EquipmentCategories.FindAsync(id);
            if (equipmentCategory != null)
            {
                _context.EquipmentCategories.Remove(equipmentCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
