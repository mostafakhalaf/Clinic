using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Helper
{
    public class TokenHandler
    {
        private const int EXPIRE_HOURS = 24;
        private const int EXPIRE_HOURS_Remember = 720;
        private static readonly byte[] Key = Encoding.ASCII.GetBytes(JWTConfigrations.TokenSecret);

        public static string CreateToken(User user, bool remember = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(remember ? EXPIRE_HOURS_Remember : EXPIRE_HOURS),
                //Expires = DateTime.UtcNow.AddMinutes(.5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
