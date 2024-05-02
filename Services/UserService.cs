public class UserService
{


    public static readonly List<User> _users = new List<User>(){
        new User{
            UserId = Guid.Parse("75424b9b-cbd4-49b9-901b-056dd1c6a020"),
            Username = "hawra_alramadan",
            FirstName = "Hawra",
            LastName = "Alramadan",
            Email = "hawra@gmail.com",
            Password = "password123",
            PhoneNumber = "966588563487",
            IsAdmin = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            },

       new User{
            UserId = Guid.Parse("75424b9b-cbd4-49b9-501b-056dd1c6a020"),
            Username = "Amani_Atiah",
            FirstName = "Amani",
            LastName = "Atiah",
            Email = "amani@gmail.com",
            Password = "password123",
            PhoneNumber = "966549563487",
            IsAdmin = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            },

     new User{
            UserId = Guid.Parse("75466b9b-cbd4-49b9-701b-056dd1c6a020"),
            Username = "Atheer_alsaedi",
            FirstName = "Atheer",
            LastName = "Alsaedi",
            Email = "atheer@gmail.com",
            Password = "password123",
            PhoneNumber = "966549553487",
            IsAdmin = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            },

        new User{
            UserId = Guid.Parse("75466b9b-cbd4-49b9-801b-056dd1c6a020"),
            Username = "Fatimah_alramadan",
            FirstName = "Fatimah",
            LastName = "Alramadan",
            Email = "fatimah@gmail.com",
            Password = "password123",
            PhoneNumber = "966533563487",
            IsAdmin = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            },

        new User{
            UserId = Guid.Parse("75466b9b-cbd4-49b9-601b-056dd1c6a020"),
            Username = "Reem_Ahmed",
            FirstName = "Reem",
            LastName = "Ahmed",
            Email = "reem@gmail.com",
            Password = "password123",
            PhoneNumber = "966567563487",
            IsAdmin = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            },

        new User{
            UserId = Guid.Parse("75466b9b-cbd4-49b9-401b-056dd1c6a020"),
            Username = "Rawabi_Khaled",
            FirstName = "Rawabi",
            LastName = "Khaled",
            Email = "rawabi@gmail.com",
            Password = "password123",
            PhoneNumber = "966579553487",
            IsAdmin = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            },

        new User{
            UserId = Guid.Parse("75466b9b-cbd4-49b9-301b-056dd1c6a020"),
            Username = "Lama_Waleed",
            FirstName = "Lama",
            LastName = "Waleed",
            Email = "lama@gmail.com",
            Password = "password123",
            PhoneNumber = "96653563487",
            IsAdmin = false,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            },
    };

    public IEnumerable<User> GetAllUsersService()
    {
        return _users;
    }

    public User? GetUserById(Guid userId)
    {
        return _users.Find(user => user.UserId == userId);

    }

    public User CreateUserService(User newUser)
    {
        newUser.UserId = Guid.NewGuid();
        newUser.CreatedAt = DateTime.Now;
        _users.Add(newUser);
        return newUser;

    }


    public User UpdateUserService(Guid userId, User updateUser)
    {
        var existingUser = _users.FirstOrDefault(u => u.UserId == userId);
        if (existingUser != null)
        {
            existingUser.Username = updateUser.Username;
            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;
            existingUser.Email = updateUser.Email;
            existingUser.Password = updateUser.Password;
            existingUser.PhoneNumber = updateUser.PhoneNumber;
            existingUser.IsAdmin = updateUser.IsAdmin;

        }
        return existingUser;


    }



    public bool DeleteUserService(Guid userId)
    {
        var userToRemove = _users.FirstOrDefault(u => u.UserId == userId);
        if (userToRemove != null)
        {
            _users.Remove(userToRemove);
            return true;     
        }
        return false;
    
    }
}