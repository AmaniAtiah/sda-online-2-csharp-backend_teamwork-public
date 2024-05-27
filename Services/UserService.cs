using Backend.Dtos;
using Backend.Dtos.Pagination;
using Backend.EntityFramework;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Backend.Middlewares;

namespace Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<PaginationResult<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize)
        {

            var totalUserAccount = await _appDbContext.Users
            .CountAsync();

            var users = await _appDbContext.Users
            .Skip((pageNumber - 1) * pageSize)
            .Where(u => !u.IsAdmin)
            .Take(pageSize)
            .ToListAsync();



            var userDtos = _mapper.Map<List<UserDto>>(users);

    
            return new PaginationResult<UserDto>
            {
                Items = userDtos,
                TotalCount = totalUserAccount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        }

        // banned and unbanned user 

       public async Task<bool> BannedAndUnbannedUserAsync(Guid userId)
    {
        var user = await _appDbContext.Users.FindAsync(userId);
        if (user != null)
        {
            user.IsBanned = !user.IsBanned;  // Toggle the ban status
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }





        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _appDbContext.Users
                //    .Include(u => u.Addresses)
        .Include(u => u.Orders)
        // products of order 
        .ThenInclude(order => order.OrderProducts)
        .ThenInclude(orderProduct => orderProduct.Product)
       
        // .Include(u => u.Cart)
        //     .ThenInclude(cart => cart.CartProducts)
        //         .ThenInclude(cartProduct => cartProduct.Product)
        .FirstOrDefaultAsync(u => u.UserId == userId);

            var userDto = _mapper.Map<UserDto>(user);
            if (userDto.IsAdmin)
            {
                // userDto.Addresses = null;
                userDto.Orders = null;
            }
            return userDto;
        }

        public async Task<UserDto> AddUserAsync(RegisterDto newUserData)
        {
            var user = new User
            {
                UserName = newUserData.UserName,
                FirstName = newUserData.FirstName,
                LastName = newUserData.LastName,
                PhoneNumber = newUserData.PhoneNumber,
                Address = newUserData.Address,
                Email = newUserData.Email,
                Password = _passwordHasher.HashPassword(null, newUserData.Password),
                IsAdmin = newUserData.IsAdmin,
                
            };
            _appDbContext.Users.Add(user);

            // try
            // {
                await _appDbContext.SaveChangesAsync();
            // }
            // catch (DbUpdateException ex)
            // {
            //     if (ex.InnerException is PostgresException postgresEx && postgresEx.SqlState == "23505")
            //     {
            //         // Unique constraint violation
            //         throw new DuplicateEmailException("A user with this email address already exists.");
            //     }
            //     throw;
            // }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                // Addresses = user.Addresses,
                // Orders = user.Orders,
            };

            // var userDto = _mapper.Map<UserDto>(user);
            // if (userDto.IsAdmin)
            // {
            //     userDto.Addresses = null;
            //     userDto.Orders = null;
            // }
            return userDto;
        }




        public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserDto userData)
        {
            var existingUser = await _appDbContext.Users.FindAsync(userId);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            existingUser.UserName = userData.UserName ?? existingUser.UserName; ;
            existingUser.FirstName = userData.FirstName ?? existingUser.FirstName;
            existingUser.LastName = userData.LastName ?? existingUser.LastName;
            existingUser.PhoneNumber = userData.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.Email = userData.Email ?? existingUser.Email;
            existingUser.Address = userData.Address?? existingUser.Address;
            // existingUser.Password = _passwordHasher.HashPassword(null, userData.Password);

            if(userData.IsAdmin.HasValue){
                existingUser.IsAdmin = userData.IsAdmin.Value;
            } 
            if(userData.IsBanned.HasValue){
                existingUser.IsBanned = userData.IsBanned.Value;
            }

            await _appDbContext.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(existingUser);
            if (userDto.IsAdmin)
            {
                // userDto.Addresses = null;
                userDto.Orders = null;
            }
            return userDto;
        }

        public async Task<UserDto?> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _appDbContext.Users
             // .Include(user => user.Addresses)
             // .Include(user => user.Orders)  
             // .Include(user => user.Cart)

             .SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var userDto = _mapper.Map<UserDto>(user);
            if (userDto.IsAdmin)
            {
                // userDto.Addresses = null;
                userDto.Orders = null;

            }
            return userDto;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var userToRemove = await _appDbContext.Users.FindAsync(userId);
            if (userToRemove != null)
            {
                _appDbContext.Users.Remove(userToRemove);
                await _appDbContext.SaveChangesAsync();
            }
        }


        // public async Task<IEnumerable<Address>> GetAllAddressesByUserIdAsync(Guid userId)
        // {
        //     return await _appDbContext.Addresses
        //      .Where(p => p.UserId == userId)
        //      .Select(p => new Address
        //      {
        //          AddressId = p.AddressId,
        //          AddressLine = p.AddressLine,
        //          City = p.City,
        //          State = p.State,
        //          Country = p.Country,
        //          ZipCode = p.ZipCode,
        //          UserId = p.UserId
        //      }).ToListAsync();
        // }
    }
}