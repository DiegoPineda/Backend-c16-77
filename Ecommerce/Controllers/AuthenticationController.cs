using Ecommerce.DbContexts;
using Ecommerce.Models.UsersDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly EcommerceContext _context; // Suponiendo que estás utilizando Entity Framework Core

        public AuthenticationController(IConfiguration configuration, EcommerceContext context)
        {
            _secretKey = configuration.GetSection("settings").GetSection("secretkey").ToString();
            _context = context;
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult Validar([FromBody] UserLogin request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

                if (user != null)
                {
                    // Usuario encontrado, se genera el token JWT
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_secretKey);

                    //-----------------

                    var isAdminClaimValue = user.Admin ? "true" : "false";
                    var isAdminClaim = new Claim("Admin", isAdminClaimValue);





                    //-------------------

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            isAdminClaim
                            // Puedes incluir más claims si lo deseas
                        }),
                        Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }
                else
                {
                    // Usuario no encontrado, se devuelve un mensaje de error
                    return BadRequest("Credenciales incorrectas.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud.");
            }
        }
    }
}