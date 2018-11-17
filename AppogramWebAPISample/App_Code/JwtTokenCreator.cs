using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;
using System.Security.Claims;

namespace AppogramController.App_Code
{
    public class JwtTokenCreator
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private string secretKey = ConfigurationManager.AppSettings["secretKey"];

        public JwtTokenCreator()
        {
            this._jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateJwtToken(IReadOnlyDictionary<string, string> payloadContents)  
        {

            var payloadClaims = payloadContents.Select(c => new Claim(c.Key, c.Value));


            var securityKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            JwtSecurityToken jwtToken = new JwtSecurityToken
            (
                issuer: "Appogram",
                claims: payloadClaims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest),
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30)
            );

            string tokenString = _jwtSecurityTokenHandler.WriteToken(jwtToken);

            return tokenString;
        }

        public bool ValidateToken(string TokenString)
        {
            Boolean result = false;

            try
            {
                SecurityToken securityToken = new JwtSecurityToken(TokenString);
                var securityKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = "Appogram",
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                ClaimsPrincipal claimsPrincipal = _jwtSecurityTokenHandler.ValidateToken(TokenString, validationParameters, out securityToken);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}