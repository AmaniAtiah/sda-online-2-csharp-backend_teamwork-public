using Microsoft.EntityFrameworkCore;
using Backend.EntityFramework;
using Backend.Models;
using Backend.Dtos;

namespace Backend.Services
{
    public class AddressService
    {
        private readonly AppDbContext _appDbContext;
        public AddressService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
        {
            return (IEnumerable<AddressDto>)await _appDbContext.Addresses
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

        public async Task<Address?> GetAddressById(Guid addressId, Guid userId)
        {
            // display address with user create using include
            return await _appDbContext.Addresses.Include(address => address.User).FirstOrDefaultAsync(address => address.AddressId == addressId && address.UserId == userId);
        }

         public async Task<Address> AddAddressService(Address newAddress, Guid userId)
        {
            newAddress.AddressId = Guid.NewGuid();
            newAddress.UserId = userId;
            _appDbContext.Addresses.Add(newAddress);
            await _appDbContext.SaveChangesAsync();
            return newAddress;
        }

        public async Task<Address?> UpdateAddressService(Guid addressId, Address updateAddress, Guid userId)
        {
            var existingAddress = await _appDbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId && a.UserId == userId);

            if (existingAddress != null)
            {
                existingAddress.AddressLine = updateAddress.AddressLine ?? existingAddress.AddressLine;
                existingAddress.City = updateAddress.City ?? existingAddress.City;
                existingAddress.State = updateAddress.State ?? existingAddress.State;
                existingAddress.Country = updateAddress.Country ?? existingAddress.Country;
                existingAddress.ZipCode = updateAddress.ZipCode ?? existingAddress.ZipCode;

                await _appDbContext.SaveChangesAsync();
            }

            // Return the updated address (or null if not found or not belonging to the user)
            return existingAddress;
        } 

        public async Task<bool> DeleteAddressService(Guid addressId, Guid userId)
        {
            var addressToRemove = await _appDbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId && a.UserId == userId);
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