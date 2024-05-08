using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.EntityFramework; // Make sure to add the appropriate namespace for your DbContext
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
            return await _dbContext.Addresses.Include(a => a.User).ToListAsync();

        }

        public async Task<Address?> GetAddressByIdAsync(Guid addressId)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(address => address.AddressId == addressId);
        }

        public async Task<Address> CreateAddressService(Address newAddress)
        {
            Address address = new Address
            {
                AddressId = Guid.NewGuid(),
                AddressLine = newAddress.AddressLine,
                City = newAddress.City,
                State = newAddress.State,
                Country = newAddress.Country,
                ZipCode = newAddress.ZipCode,
                UserId = newAddress.UserId
            };
            await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();
            return address;
        }

        public async Task<Address> UpdateAddressService(Guid addressId, Address updateAddress)
        {
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);
            if (existingAddress != null)
            {
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
