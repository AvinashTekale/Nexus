using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nexus.Data;
using Nexus.DTOs;
using Nexus.Entities;
using Nexus.Interfaces;

namespace Nexus.Repositories
{

    public class ContactPersonRepository : IContactPersonRepository
    {
        private readonly NexusDBContext _context;

        public ContactPersonRepository(NexusDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactPersonDTO>> GetAllAsync()
        {
            return await _context.ContactPersons
                .Select(cp => new ContactPersonDTO
                {
                    Id = cp.Id,
                    Name = cp.User.Name,
                    Phone = cp.Phone,
                    Email = cp.User.Email,
                    Password = "", // Password is not exposed in DTO.
                    PartnerId = cp.PartnerId,
                    Role = cp.User.Role
                })
                .ToListAsync();
        }

        public async Task<ContactPersonDTO> GetByIdAsync(int id)
        {
            var contactPerson = await _context.ContactPersons
                .Include(cp => cp.User)
                .FirstOrDefaultAsync(cp => cp.Id == id);

            if (contactPerson == null) return null;

            return new ContactPersonDTO
            {
                Id = contactPerson.Id,
                Name = contactPerson.User.Name,
                Phone = contactPerson.Phone,
                Email = contactPerson.User.Email,
                Password = "", // Password is not exposed in DTO.
                PartnerId = contactPerson.PartnerId,
                Role = contactPerson.User.Role
            };
        }

        public async Task AddAsync(ContactPersonDTO contactPersonDto)
        {
            var user = new User
            {
                Name = contactPersonDto.Name,
                Email = contactPersonDto.Email,
                Role = contactPersonDto.Role,
                PasswordHash = HashPassword(contactPersonDto.Password), // Add password hashing logic here.
                PasswordKey = GenerateKey() // Add key generation logic here.
            };

            var contactPerson = new ContactPerson
            {
                User = user,
                PartnerId = contactPersonDto.PartnerId,
                Phone = contactPersonDto.Phone,
            };

            await _context.ContactPersons.AddAsync(contactPerson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactPersonDTO contactPersonDto)
        {
            var contactPerson = await _context.ContactPersons
                .Include(cp => cp.User)
                .FirstOrDefaultAsync(cp => cp.Id == contactPersonDto.Id);

            if (contactPerson != null)
            {
                contactPerson.User.Name = contactPersonDto.Name;
                contactPerson.User.Email = contactPersonDto.Email;
                contactPerson.User.Role = contactPersonDto.Role;

                // Update the password only if it's provided
                if (!string.IsNullOrEmpty(contactPersonDto.Password))
                {
                    contactPerson.User.PasswordHash = HashPassword(contactPersonDto.Password); // Add password hashing logic here.
                    contactPerson.User.PasswordKey = GenerateKey(); // Add key generation logic here.
                }

                contactPerson.PartnerId = contactPersonDto.PartnerId;
                contactPerson.Phone = contactPersonDto.Phone;

                _context.ContactPersons.Update(contactPerson);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var contactPerson = await _context.ContactPersons
                .Include(cp => cp.User)
                .FirstOrDefaultAsync(cp => cp.Id == id);

            if (contactPerson != null)
            {
                _context.Users.Remove(contactPerson.User); // Remove associated User.
                _context.ContactPersons.Remove(contactPerson);
                await _context.SaveChangesAsync();
            }
        }

        private byte[] HashPassword(string password)
        {
            // Add password hashing logic here (e.g., using HMACSHA512).
            return System.Text.Encoding.UTF8.GetBytes(password); // Placeholder.
        }

        private byte[] GenerateKey()
        {
            // Add logic to generate a password key.
            return System.Text.Encoding.UTF8.GetBytes("Key"); // Placeholder.
        }
    }

}
