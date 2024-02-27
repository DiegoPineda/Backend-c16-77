using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.UsersDto;
using Ecommerce.Services;
using Ecommerce.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usersFromRepo = await _userRepository.getAllUsersAsync();
            var usersDto = _mapper.Map<List<UsuarioDto>>(usersFromRepo);

            return Ok(usersDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
  
        public async Task<ActionResult<Users>> CreateUser([FromBody]UserForCreationDto user)
        {
            try
            {
                var newUser = _mapper.Map<Users>(user);
                await _userRepository.AddUsersAsync(newUser);
                return CreatedAtAction("GetIdUserAsync", new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }

        [HttpGet("{id}", Name ="GetUser")]
        public async Task<ActionResult<UsuarioDto>> GetIdUserAsync(int id)
        {
            var userId = await _userRepository.UsersAsync(id);

            if (userId == null)
            {
                return NotFound(); // Usuario no encontrado
            }

            var responseId = _mapper.Map<UsuarioDto>(userId);
            return Ok(responseId);
        }


    }
}
