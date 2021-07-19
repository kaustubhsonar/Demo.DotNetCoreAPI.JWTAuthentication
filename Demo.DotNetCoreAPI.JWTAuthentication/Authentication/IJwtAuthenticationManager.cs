using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DotNetCoreAPI.JWTAuthentication.Authentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);        
    }


    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private Dictionary<string, string> users = new Dictionary<string, string> { { "user1", "password1" }, { "user2", "password2" } };

        private readonly string _key;
        public JwtAuthenticationManager(string key)
        {
            this._key = key;
        }        
        public string Authenticate(string username, string password)
        {
            if (!users.Any(x => x.Key == username && x.Value == password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDecriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
