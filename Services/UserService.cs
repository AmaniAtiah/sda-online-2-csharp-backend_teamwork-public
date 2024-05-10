
using Backend.Dtos;
using Backend.Dtos.Pagination;
using Backend.Dtos.User;
using Backend.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Backend.Models;
using Microsoft.AspNetCore.Identity;
=======
>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182

namespace Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
<<<<<<< HEAD
=======

>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182
        public UserService(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }



        public async Task<PaginationResult<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalUserAccount = await _appDbContext.Users.CountAsync();
                var users = await _appDbContext.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserDto
                {
<<<<<<< HEAD
                    UserId = Guid.NewGuid(),
                    UserName = newUser.UserName,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Password = _passwordHasher.HashPassword(null, newUser.Password),
                    PhoneNumber = newUser.PhoneNumber,
                    IsAdmin = newUser.IsAdmin,
                    CreatedAt = newUser.CreatedAt,
                    UpdatedAt = newUser.UpdatedAt,
=======
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    IsAdmin = user.IsAdmin,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                }).ToListAsync();

                return new PaginationResult<UserDto>
                {
                    Items = users,
                    TotalCount = totalUserAccount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182
                };
            }
            catch (Exception e)
            {
                throw new Exception("An error occured");

            }
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _appDbContext.Users
                .Where(user => user.UserId == userId)
                .Select(user => new UserDto
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



                }).FirstOrDefaultAsync();
                return user;

            }
            catch (Exception e)
            {
                throw new Exception("An error occured");
            }
        }
        public async Task<UserDto> CreateUserAsync(CreateUserDto newUserData)
        {
            try
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


                };
                return newUserDto;



            }
            catch (DbUpdateException e)
            {
                throw new InvalidOperationException("could not save the user to daatabase", e);
            }
        }

        public async Task<UserDto?> LoginUserAsync(LoginDto loginDto)
        {
            try
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
            catch (Exception e)
            {
                throw new Exception("An error occured");
            }
        }

    }
}