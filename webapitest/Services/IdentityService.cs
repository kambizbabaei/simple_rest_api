using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using webapitest.Domains;
using webapitest.other;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace webapitest.Services
{
    public class IdentityService:IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly JWTSettings _jwtSettings;

        public IdentityService(UserManager<User> userManager, JWTSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> RegisterAsync(string requestEmail, string requestPassword)
        {
            var user = await _userManager.FindByEmailAsync(requestEmail);
            if (user is not null)
            {
                return String.Empty;
            }

            user = new User()
            {
                Email = requestEmail,
                UserName = requestEmail
            };
            var createdUser = await _userManager.CreateAsync(user, requestPassword);
            if (!createdUser.Succeeded)
            {
                return string.Empty;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim("Id",user.Id.ToString())
                }),
                SigningCredentials =new SigningCredentials (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> LoginAsync(string requestEmail, string requestPassword)
        {
            var user = await _userManager.FindByEmailAsync(requestEmail);
            if (user is null)
            {
                return String.Empty;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim("Id",user.Id.ToString())
                }),
                SigningCredentials =new SigningCredentials (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}