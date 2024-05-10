using Microsoft.EntityFrameworkCore;
using Backend.EntityFramework;
using Backend.Helpers;
using Backend.Models;

namespace Backend.Services
{
    public class AddressService
    {
        private readonly AppDbContext _appDbContext;
        public AddressService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await _appDbContext.Addresses
             .Select(p => new Address
             {
                 AddressId = p.AddressId,
                 AddressLine = p.AddressLine,
                 City = p.City,
                 State = p.State,
                 Country = p.Country,
                 ZipCode = p.ZipCode,
                 UserId = p.UserId
             }).ToListAsync();
        }

        public async Task<Address?> GetAddressById(Guid addressId)
        {
            return await _appDbContext.Addresses.FindAsync(addressId);
        }

        public async Task<Address> AddAddressService(Address newAddress)
        {
            newAddress.AddressId = Guid.NewGuid();
            _appDbContext.Addresses.Add(newAddress);
            await _appDbContext.SaveChangesAsync();
            return newAddress;
        }

        public async Task<Address?> UpdateAddressService(Guid addressId, Address updateAddress)
        {
            var existingAddress = await _appDbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
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
                await _appDbContext.SaveChangesAsync();
            }
            return existingAddress;
        }
        public async Task<bool> DeleteAddressService(Guid addressId)
        {
            var addressToRemove = await _appDbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
            if (addressToRemove != null)
            {
                _appDbContext.Addresses.Remove(addressToRemove);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}