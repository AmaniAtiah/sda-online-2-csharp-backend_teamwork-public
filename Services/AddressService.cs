// using Microsoft.EntityFrameworkCore;
// using Backend.EntityFramework;
// using Backend.Dtos;

// namespace Backend.Services
// {
//     public class AddressService
//     {
//         private readonly AppDbContext _appDbContext;
//         public AddressService(AppDbContext appDbContext)
//         {
//             _appDbContext = appDbContext;
//         }

//         public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
//         {
//             return await _appDbContext.Addresses
//         .Select(p => new AddressDto
//         {
//             AddressId = p.AddressId,
//             AddressLine = p.AddressLine,
//             City = p.City,
//             State = p.State,
//             Country = p.Country,
//             ZipCode = p.ZipCode,
//             UserId = p.UserId
//         })
//         .ToListAsync();
//         }

//         public async Task<Address?> GetAddressById(Guid addressId, Guid userId)
//         {
//             // display address with user create using include
//             return await _appDbContext.Addresses
//             .Include(address => address.User)
//             // then include user has many addresses
//             .ThenInclude(user => user.Addresses)
//             .Include(address => address.Orders)
            
//             // then include user has many orders


//             .FirstOrDefaultAsync(address => address.AddressId == addressId && address.UserId == userId);

//         }

//         // admin can show one address
//         public async Task<Address?> ShowAddressByAdmin(Guid addressId)
//         {
//             return await _appDbContext.Addresses.Include(address => address.User)
//             .Include(address => address.Orders).FirstOrDefaultAsync(address => address.AddressId == addressId);
//         }
//         public async Task<Address> AddAddressService(Address newAddress, Guid userId)
//         {
//             newAddress.AddressId = Guid.NewGuid();
//             newAddress.UserId = userId;
//             _appDbContext.Addresses.Add(newAddress);
//             await _appDbContext.SaveChangesAsync();
//             return newAddress;
//         }

//         public async Task<Address?> UpdateAddressService(Guid addressId, Address updateAddress)
//         {
//             var existingAddress = await _appDbContext.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);

//             if (existingAddress != null)
//             {
//                 existingAddress.AddressLine = updateAddress.AddressLine ?? existingAddress.AddressLine;
//                 existingAddress.City = updateAddress.City ?? existingAddress.City;
//                 existingAddress.State = updateAddress.State ?? existingAddress.State;
//                 existingAddress.Country = updateAddress.Country ?? existingAddress.Country;
//                 existingAddress.ZipCode = updateAddress.ZipCode ?? existingAddress.ZipCode;
//                 existingAddress.UserId = existingAddress.UserId;

//                 await _appDbContext.SaveChangesAsync();
//             }

//             // Return the updated address (or null if not found or not belonging to the user)
//             return existingAddress;
//         }

//         public async Task<bool> DeleteAddressService(Guid addressId)
//         {
//             var addressToRemove = await _appDbContext.Addresses.FindAsync(addressId);
//             if (addressToRemove != null)
//             {
//                 _appDbContext.Addresses.Remove(addressToRemove);
//                 await _appDbContext.SaveChangesAsync();
//                 return true;
//             }
//             return false;
//         }
//     }
// }