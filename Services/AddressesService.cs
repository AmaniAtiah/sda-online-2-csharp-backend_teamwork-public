using Microsoft.EntityFrameworkCore;
using Backend.EntityFramework;
using Backend.Helpers;
using Backend.Models;

namespace Backend.Services
{
    public class AddressesService
    {
        private readonly AppDbContext _dbContext;

        public AddressesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await _dbContext.Addresses.ToListAsync();

        }

        public async Task<Address?> GetAddressById(Guid addressId)
        {
            return await _dbContext.Addresses.FindAsync(addressId);
        }

        public async Task<Address> CreateAddressService(Address newAddress)
        {
            newAddress.AddressId = Guid.NewGuid();
            _dbContext.Addresses.Add(newAddress);
            await _dbContext.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> UpdateAddressService(Guid addressId, Address updateAddress)
        {
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
            if (existingAddress != null)
            {
                existingAddress.AddressLine = updateAddress.AddressLine ?? existingAddress.AddressLine;
                existingAddress.City = updateAddress.City ?? existingAddress.City;
                existingAddress.State = updateAddress.State ?? existingAddress.State;
                existingAddress.Country = updateAddress.Country ?? existingAddress.Country;
                existingAddress.ZipCode = updateAddress.ZipCode ?? existingAddress.ZipCode;
                existingAddress.AddressLine = updateAddress.AddressLine ?? existingAddress.AddressLine;
                existingAddress.City = updateAddress.City ?? existingAddress.City;
                existingAddress.State = updateAddress.State ?? existingAddress.State;
                existingAddress.Country = updateAddress.Country ?? existingAddress.Country;
                existingAddress.ZipCode = updateAddress.ZipCode ?? existingAddress.ZipCode;
                existingAddress.UserId = updateAddress.UserId;
                await _dbContext.SaveChangesAsync();
            }
            return existingAddress;
        }
        public async Task<bool> DeleteAddressService(Guid addressId)
        {
            var addressToRemove = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
            if (addressToRemove != null)
            {
                _dbContext.Addresses.Remove(addressToRemove);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}