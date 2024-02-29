using Ecommerce.Services.Orderservice;
using Ecommerce.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        public OrderController(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateOrderFromCart(int userId)
        {
            var user = await _userRepository.UsersAsync(userId);

            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var order = await _orderRepository.CreateOrderFromCartAsync(user);

            if (order == null)
            {
                return BadRequest("No se puede crear un pedido vacío");
            }

            return Ok(order);
        }
    }
}
