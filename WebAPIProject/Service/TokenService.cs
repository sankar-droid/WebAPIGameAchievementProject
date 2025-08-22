
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIProject.Interface;
using WebAPIProject.Models;

namespace CodeFirstApproach.Service
{
    public class TokenService : IToken
    {
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public TokenService(IConfiguration configuration)
        {
            _symmetricSecurityKey = new
SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]!));
        }
        public string GenerateToken(User user)
        {
            string token = string.Empty;
            var claims = new List<Claim>
           {
new Claim(JwtRegisteredClaimNames.NameId,user.Name),
new Claim(JwtRegisteredClaimNames.Email,user.Email),

};
            var cred = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = cred
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var myToken = tokenHandler.CreateToken(tokenDescription);
            token = tokenHandler.WriteToken(myToken);
            return token;



        }
    }
}
