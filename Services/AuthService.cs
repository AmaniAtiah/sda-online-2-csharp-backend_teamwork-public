using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            Console.WriteLine($"{_configuration["Jwt:Issuer"]}");
        }

        public string GenerateJwt(UserDto user)
        {

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);//the key from configuration

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { //Descripe the token through useing claim
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin? "Admin" : "User"),//using role to specify user and admin
            }),

                Expires = DateTime.UtcNow.AddMinutes(2),//how many hours token will be vaild
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler(); // create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}