using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.ProductDto;
using Ecommerce.Models.UsersDto;
using Ecommerce.Services.CartService;
using Ecommerce.Services.UserService;
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
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usersFromRepo = await _userRepository.getAllUsersAsync();
            var usersDto = _mapper.Map<List<UsuarioDto>>(usersFromRepo);

            return Ok(usersDto);
        }
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(UserForCreationDto user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null");
            }

            var newUser = _mapper.Map<Users>(user);

            // Crear un nuevo carrito asociado al nuevo usuario
            var cart = new Cart
            {
                UserId = newUser.Id  // Establecer el UserId del carrito como el Id del nuevo usuario
            };

            // Inicializar la lista de ítems del carrito
            cart.Items = new List<CartItem>();

            // Asociar el carrito creado al nuevo usuario
            newUser.Cart = cart;

            // Agregar el nuevo usuario a la base de datos
            _userRepository.AddUsersAsync(newUser);

            // Guardar los cambios en la base de datos
            await _userRepository.SaveAsync();
            _cartRepository.UpdateCartAsync(cart);
            await _cartRepository.SaveAsync();

            // Retornar el nuevo usuario creado
            return CreatedAtAction(nameof(GetIdUserAsync), new { id = newUser.Id }, newUser);
        }



        [HttpGet("{id}", Name = "GetUser")]
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
