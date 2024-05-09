using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Helpers;
using Backend.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;
        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _appDbContext.Users.Include(user => user.Addresses).Include(u => u.Orders).ToListAsync();
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving users.", e);
            }
        }

        public async Task<User?> GetUserAsync(Guid UserId)
        {
            try
            {
                return await _appDbContext.Users.FindAsync(UserId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving users.", e);
            }

        }

        public async Task<User> AddUserAsync(User newUser)
        {
            try
            {
                User user = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = newUser.UserName,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Password = newUser.Password,
                    PhoneNumber = newUser.PhoneNumber,
                    IsAdmin = newUser.IsAdmin,
                    CreatedAt = newUser.CreatedAt,
                    UpdatedAt = newUser.UpdatedAt,
                };
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while adding user.", e);
            }
        }

        public async Task<User> UpdateUserAsync(Guid UserId, User newUser)
        {
            try
            {
                var existingUser = await _appDbContext.Users.FindAsync(UserId);

                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                existingUser.UserName = newUser.UserName;
                existingUser.FirstName = newUser.FirstName;
                existingUser.LastName = newUser.LastName;
                existingUser.Email = newUser.Email;
                existingUser.Password = newUser.Password;
                existingUser.PhoneNumber = newUser.PhoneNumber;
                existingUser.IsAdmin = newUser.IsAdmin;
                existingUser.UpdatedAt = DateTime.UtcNow;

                await _appDbContext.SaveChangesAsync();

                return existingUser;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while updating user.", e);

            }
        }

        public async Task<bool> DeleteUserAsync(Guid UserId)
        {
            try
            {
                var existingUser = await _appDbContext.Users.FindAsync(UserId);

                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                _appDbContext.Users.Remove(existingUser);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while deleting user.", e);

            }
        }
    }
}