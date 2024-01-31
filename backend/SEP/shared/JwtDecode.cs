using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class JwtDecode
    {
        public static string DecodeToken(string authorizationHeader)
        {
            var token = authorizationHeader.Replace("Bearer ", string.Empty);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var user = jsonToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            return user;
        }
    }
}
