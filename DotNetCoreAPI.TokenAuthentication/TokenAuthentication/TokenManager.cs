using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAPI.TokenAuthentication.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private List<Token> listTokens;
        public TokenManager()
        {
            listTokens = new List<Token>();
        }

        public bool Authenticate(string username, string password)
        {
            if (username.ToLower() == "admin" && password == "password")
                return true;
            else
                return false;
        }

        public Token NewToken()
        {

            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.Now.AddMinutes(2)
            };
            listTokens.Add(token);
            return token;
        }

        public bool VerifyToken(string token)
        {

            if (listTokens.Any(x => x.Value == token && x.ExpiryDate > DateTime.Now))
                return true;
            else
                return false;
        }
    }
}
