using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Authentication
{
    public class TokenManager : ITokenManager
    {
        private IConfiguration _config;

        public TokenManager(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJwtToken()
        {
            //security key
            string securityKey = _config["Jwt:Key"];
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //add claims
            var claims = new List<Claim>();
            claims.Add(new Claim("Dummy", "Value"));

            //create token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddDays(100),
                signingCredentials: signingCredentials
                , claims: claims
            );

            //return token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
