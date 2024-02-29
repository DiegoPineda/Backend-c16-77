using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.UsersDto;
using Ecommerce.Services.CartService;
using Ecommerce.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;

        public UserController(IUserRepository userRepository, IMapper mapper, ICartRepository cart)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cartRepository = cart;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usersFromRepo = await _userRepository.getAllUsersAsync();
            var usersDto = _mapper.Map<List<UsuarioDto>>(usersFromRepo);

            return Ok(usersDto);
        }
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(UserForCreationDto user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }

            var newUser = _mapper.Map<Users>(user);



            var newCart = new Cart();
            newUser.Cart = newCart; // Asociar el carrito con el usuario

            // Agregar el carrito a la base de datos
            _cartRepository.AddCartAsync(newCart);



            // Agregar el nuevo usuario a la base de datos
            _userRepository.AddUsersAsync(newUser);

            // Guardar los cambios en la base de datos
            await _userRepository.SaveAsync();




            // Retornar el nuevo usuario creado
            return CreatedAtAction(nameof(GetIdUserAsync), new { id = newUser.Id }, newUser);
        }


        
        [HttpGet("{id}", Name = "GetUser")]
        [Authorize]
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
