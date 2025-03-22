using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.DTOs;
using Nexus.Entities;
using Nexus.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Nexus.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly NexusDBContext _context;

        public HospitalRepository(NexusDBContext context)
        {
            _context = context;
        }

        // Get all hospitals
        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _context.Hospitals
                                 .Include(h => h.User)
                                 .Include(h => h.City)
                                 .Include(h => h.City.State)
                                 .ToListAsync();
        }

        // Get hospital by ID
        public async Task<Hospital> GetByIdAsync(int id)
        {
            return await _context.Hospitals
                                 .Include(h => h.User)
                                 .Include(h => h.City)
                                 .Include(h => h.City.State)
                                 .FirstOrDefaultAsync(h => h.Id == id);
        }

        // Create hospital and user
        public async Task<Hospital> CreateAsync(HospitalDTO dto)
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

            var hospital = new Hospital
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Address = dto.Address,
                CityId = dto.CityId,
                NumberOfBME = dto.NumberOfBME,
                NumberOfBeds = dto.NumberOfBeds,
                UserId = user.Id,
            };

            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();

            return hospital;
        }

        // Update hospital
        public async Task<Hospital> UpdateAsync(int id, HospitalDTO dto)
        {
            var hospital = await _context.Hospitals
                                         .Include(h => h.User)
                                         .FirstOrDefaultAsync(h => h.Id == id);

            if (hospital == null) return null;

            // Update hospital properties
            hospital.Name = dto.Name;
            hospital.Phone = dto.Phone;
            hospital.Address = dto.Address;
            hospital.CityId = dto.CityId;
            hospital.NumberOfBME = dto.NumberOfBME;
            hospital.NumberOfBeds = dto.NumberOfBeds;

            // Update related user

            var hmac = new HMACSHA512();
            hospital.User.Email = dto.Email;
            hospital.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            hospital.User.PasswordKey = hmac.Key;

            await _context.SaveChangesAsync();

            return hospital;
        }

        // Delete hospital
        public async Task<bool> DeleteAsync(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null) return false;

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return true;
        }

        public IEnumerable<State> GetStates()
        {
            return _context.States.ToList();
        }

        public IEnumerable<City> GetCitiesByState(int stateId)
        {
            return _context.Cities.Where(r => r.StateID == stateId).ToList();
        }
    }

}
