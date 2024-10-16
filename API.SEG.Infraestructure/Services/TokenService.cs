using API.SEG.Aplicacion.Interface.Infraestructure;
using API.SEG.Infraestructure.Services.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.SEG.Infraestructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> BuildToken(string userName, string idSession, int expireMinutes)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Config:Secret"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(TiposClaims.IdSession, idSession)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _configuration["Config:Issuer"],
                    Audience = _configuration["Config:Audience"]
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la generacion de Token: {ex.Message}", ex);
            }
        }

        public async Task<string> GetIdSessionFromToken(string tokenString)
        {
            try
            {
                if (tokenString.StartsWith("Bearer "))
                    tokenString = tokenString.Substring("Bearer ".Length).Trim();

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                if (jwtToken != null)
                {
                    var idSessionClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == TiposClaims.IdSession);

                    if (idSessionClaim != null)
                        return idSessionClaim.Value;
                }

                throw new UnauthorizedAccessException("No se pudo obtener el Id Session del token.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
