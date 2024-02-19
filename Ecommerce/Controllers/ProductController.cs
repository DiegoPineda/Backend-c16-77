using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IEcommerceRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IEcommerceRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery] int? categoryId,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var productsQuery = _productRepository.GetProducts();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= maxPrice.Value);
            }

            var totalCount = await productsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var products = await productsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var productsDto = _mapper.Map<List<ProductForListDto>>(products);

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Products = productsDto
            });
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }


        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct(ProductForCreationDto productForCreationDto)
        {
            if (productForCreationDto == null)
            {
                return BadRequest("ProductForCreationDto cannot be null");
            }

            var productEntity = _mapper.Map<Product>(productForCreationDto);
            _productRepository.AddProduct(productEntity);
            await _productRepository.SaveAsync();

            var productResponseDto = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtAction(nameof(GetProduct), new { id = productResponseDto.Id }, productResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(productDto.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _mapper.Map(productDto, existingProduct);
            _productRepository.UpdateProduct(existingProduct);
            await _productRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(existingProduct);
            await _productRepository.SaveAsync();

            return NoContent();
        }
    }
}
