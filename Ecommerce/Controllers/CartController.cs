using Ecommerce.Entities;
using Ecommerce.Services.CartService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost("{userId}/addProduct/{productId}")]
        public async Task<IActionResult> AddProductToCart(int userId, int productId, int quantity)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);

                if (cart == null)
                {
                    return NotFound("Cart not found for the user");
                }

                // Aquí deberías agregar lógica para verificar la existencia del producto y su disponibilidad

                var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

                if (cartItem != null)
                {
                    // Si el producto ya existe en el carrito, aumenta la cantidad
                    cartItem.Quantity++;
                }
                else
                {
                    // Si el producto no está en el carrito, agrégalo
                    cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
                }

                await _cartRepository.UpdateCartAsync(cart);

                return Ok("Product added to cart successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{userId}/removeProduct/{productId}")]
        public async Task<IActionResult> RemoveProductFromCart(int userId, int productId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);

                if (cart == null)
                {
                    return NotFound("Cart not found for the user");
                }

                var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

                if (cartItem != null)
                {
                    // Si la cantidad es 1, elimina el elemento del carrito
                    cart.Items.Remove(cartItem);

                    await _cartRepository.UpdateCartAsync(cart);

                    return Ok("Product removed from cart successfully");
                }
                else
                {
                    return NotFound("Product not found in cart");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
