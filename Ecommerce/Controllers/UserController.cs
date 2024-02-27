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
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> getIdUserAsync(int id)
        {
            if(id == 0) { return BadRequest(); }

            var userId = await _userRepository.UsersAsync(id);
            var responseid = _mapper.Map<UsuarioDto>(userId);
            return Ok(responseid);
        }
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(UserForCreationDto user)
        {
            
            var newUser = _mapper.Map<Users>(user);
            await _userRepository.AddUsersAsync(newUser);
            return CreatedAtAction(nameof(getIdUserAsync), new { id = newUser.Id }, newUser);
        }


    }
}
