using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public class AuthService
    {

        public AuthService()
        {
            Console.WriteLine($"JWT Issuer: {Environment.GetEnvironmentVariable("Jwt__Issuer")}");
        }

        public string GenerateJwt(UserDto user)
        {

            var jwtKey = Environment.GetEnvironmentVariable("Jwt__key") ?? throw new
            InvalidOperationException("JWT Key is missing in environment variable.");
            var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer") ?? throw new
            InvalidOperationException("JWT Issuer is missing in environment variable.");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT__Audience") ?? throw new
            InvalidOperationException("JWT Audience is missing in environment variable.");

            var key = Encoding.ASCII.GetBytes(jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { //Descripe the token through useing claim
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin? "Admin" : "User"),//using role to specify user and admin
            }),

                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Issuer = jwtIssuer,
                Audience = jwtAudience,
            };

            var tokenHandler = new JwtSecurityTokenHandler(); // create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}