using Backend.Dtos;
using Backend.Dtos.Pagination;
using Backend.Dtos.User;
using Backend.EntityFramework;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

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
            var totalUserAccount = await _appDbContext.Users.CountAsync();
            var users = await _appDbContext.Users
            .Skip((pageNumber - 1) * pageSize)
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

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _appDbContext.Users.FindAsync(userId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> AddUserAsync(CreateUserDto newUserData)
        {
            var user = new User
            {
                UserName = newUserData.UserName,
                FirstName = newUserData.FirstName,
                LastName = newUserData.LastName,
                PhoneNumber = newUserData.PhoneNumber,
                Email = newUserData.Email,
                Password = _passwordHasher.HashPassword(null, newUserData.Password),
                IsAdmin = newUserData.IsAdmin,
            };
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            var newUserDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Addresses = user.Addresses,
                Orders = user.Orders
            };
            return newUserDto;
        }

        public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserDto userData)
        {
            var existingUser = await _appDbContext.Users.FindAsync(userId);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            existingUser.UserName = userData.UserName;
            existingUser.FirstName = userData.FirstName;
            existingUser.LastName = userData.LastName;
            existingUser.PhoneNumber = userData.PhoneNumber;
            existingUser.Email = userData.Email;
            existingUser.Password = _passwordHasher.HashPassword(null, userData.Password);

            await _appDbContext.SaveChangesAsync();

            var updatedUserDto = new UserDto
            {
                UserId = existingUser.UserId,
                UserName = existingUser.UserName,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                PhoneNumber = existingUser.PhoneNumber,
                Email = existingUser.Email,
                IsAdmin = existingUser.IsAdmin,
                CreatedAt = existingUser.CreatedAt,
                UpdatedAt = existingUser.UpdatedAt,
                Addresses = existingUser.Addresses,
                Orders = existingUser.Orders
            };
            return updatedUserDto;
        }

        public async Task<UserDto?> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var userDto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };
            return userDto;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var userToRemove = await _appDbContext.Users.FindAsync(userId);
            if (userToRemove!= null)
            {
                _appDbContext.Users.Remove(userToRemove);
                await _appDbContext.SaveChangesAsync();
            }
        }


         public async Task<IEnumerable<Address>> GetAllAddressesByUserIdAsync(Guid userId)
        {
            return await _appDbContext.Addresses
             .Where(p => p.UserId == userId)
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
    }
}