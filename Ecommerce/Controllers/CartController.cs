using Ecommerce.Services;
using Ecommerce.Services.CartService;
using Ecommerce.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IEcommerceRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CartController(ICartRepository cartRepository, IEcommerceRepository productRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int userId, int productId, int quantity)
        {
            // Supongamos que tienes un método en el repositorio de usuarios para obtener un usuario por su ID
            var user = await _userRepository.UsersAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Luego, añade el producto al carrito del usuario
            await _cartRepository.AddProductToCartAsync(user, productId, quantity);

            return Ok("Producto añadido al carrito exitosamente");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            // Supongamos que tienes un método en el repositorio de usuarios para obtener un usuario por su ID
            var user = await _userRepository.UsersAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Eliminar el producto del carrito del usuario
            await _cartRepository.RemoveProductFromCartAsync(user, productId);

            return Ok("Producto eliminado del carrito exitosamente");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return NotFound("No se encontró un carrito para el usuario especificado");
            }

            return Ok(cart);
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var user = await _userRepository.UsersAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            await _cartRepository.ClearCartAsync(user);

            return Ok("Carrito vaciado exitosamente");
        }

    }
}
