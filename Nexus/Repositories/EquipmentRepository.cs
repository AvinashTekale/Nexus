using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.Entities;
using Nexus.Interfaces;

namespace Nexus.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly NexusDBContext _context;

        public EquipmentRepository(NexusDBContext context)
        {
            _context = context;
        }

        // Get all equipment for a specific hospital
        public async Task<IEnumerable<Equipment>> GetAllAsync(int hospitalId)
        {
            return await _context.Equipments
                                 .Where(e => e.HospitalId == hospitalId)
                                 .Include(e => e.EquipmentModel)
                                 .Include(e => e.EquipmentCategory)
                                 .Include(e => e.Hospital)
                                 .Include(e => e.Department)
                                 .ToListAsync();
        }

        // Get equipment by its Serial Number (GUID)
        public async Task<Equipment> GetByIdAsync(string serialNumber)
        {
            return await _context.Equipments
                                 .Where(e => e.SerialNumber == serialNumber)
                                 .Include(e => e.EquipmentModel)
                                 .Include(e => e.EquipmentCategory)
                                 .Include(e => e.Hospital)
                                 .Include(e => e.Department) // Include Department data
                                 .FirstOrDefaultAsync();
        }

        // Add a new equipment record
        public async Task AddAsync(Equipment equipment)
        {
            await _context.Equipments.AddAsync(equipment);
            await _context.SaveChangesAsync();
        }

        // Update an existing equipment record
        public async Task UpdateAsync(Equipment equipment)
        {
            _context.Equipments.Update(equipment);
            await _context.SaveChangesAsync();
        }

        // Delete equipment by its Serial Number (GUID)
        public async Task DeleteAsync(string serialNumber)
        {
            var equipment = await _context.Equipments.FindAsync(serialNumber);
            if (equipment != null)
            {
                _context.Equipments.Remove(equipment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
