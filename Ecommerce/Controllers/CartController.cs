using Ecommerce.Entities;
using Ecommerce.Services;
using Ecommerce.Services.CartService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IEcommerceRepository _productRepository;

        public CartController(ICartRepository cartRepository, IEcommerceRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
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
                var product = await _productRepository.GetProductByIdAsync(productId);

                if (product == null)
                {
                    return NotFound("Product not found");
                }

                if (product.Stock < quantity)
                {
                    return BadRequest("Not enough stock available");
                }

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
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);

                if (cart == null)
                {
                    return NotFound("Cart not found for the user");
                }
                cart.TotalPrice = CalculateTotalPrice(cart);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        private decimal CalculateTotalPrice(Cart cart)
        {
            if (cart.Items == null)
                return 0;

            return cart.Items.Sum(item => item.Product.Price * item.Quantity);
        }

    }
}
