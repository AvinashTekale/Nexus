using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.DTOs;
using Nexus.Entities;
using Nexus.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Nexus.Repositories
{
    public class BMEAccountRepository : IBMEAccountRepository
    {
        private readonly NexusDBContext _context;

        public BMEAccountRepository(NexusDBContext context)
        {
            _context = context;
        }

        // Get all BMEAccounts
        public async Task<IEnumerable<BMEAccount>> GetAllAsync(int HospitalId)
        {
            return await _context.BMEAccounts.Where(r => r.HospitalId == HospitalId)
                                 .Include(h => h.User)
                                 .ToListAsync();
        }

        // Get BMEAccount by ID
        public async Task<BMEAccount> GetByIdAsync(int id)
        {
            return await _context.BMEAccounts
                                 .Include(h => h.User)
                                 .FirstOrDefaultAsync(h => h.Id == id);
        }

        // Create BMEAccount and user
        public async Task<BMEAccount> CreateAsync(BMEAccountDTO dto)
        {
            var hmac = new HMACSHA512();
            User user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordKey = hmac.Key,
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var bmeAccount = new BMEAccount
            {
                Name = dto.Name,
                Phone = dto.Phone,
                UserId = user.Id,
                HospitalId = dto.HospitalId
            };

            _context.BMEAccounts.Add(bmeAccount);
            await _context.SaveChangesAsync();

            return bmeAccount;
        }

        // Update BMEAccount
        public async Task<BMEAccount> UpdateAsync(int id, BMEAccountDTO dto)
        {
            var bmeAccount = await _context.BMEAccounts
                                         .Include(h => h.User)
                                         .FirstOrDefaultAsync(h => h.Id == id);

            if (bmeAccount == null) return null;

            // Update hospital properties
            bmeAccount.Name = dto.Name;
            bmeAccount.Phone = dto.Phone;
            bmeAccount.HospitalId = dto.HospitalId;

            // Update related user

            var hmac = new HMACSHA512();
            bmeAccount.User.Email = dto.Email;
            bmeAccount.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            bmeAccount.User.PasswordKey = hmac.Key;

            await _context.SaveChangesAsync();

            return bmeAccount;
        }

        // Delete BMEAccount
        public async Task<bool> DeleteAsync(int id)
        {
            var bmeAccount = await _context.BMEAccounts.FindAsync(id);
            if (bmeAccount == null) return false;

            _context.BMEAccounts.Remove(bmeAccount);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
