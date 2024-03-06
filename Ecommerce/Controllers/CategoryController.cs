using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.CategoryDto;
using Ecommerce.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategory()
        {
            var category = await _categoryRepository.GetAllCategoriesAsync();
            var responseDto = _mapper.Map<List<CategoryDto>>(category);

            return Ok(responseDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<CategoryDto>(category);
            return Ok(productDto);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Category>> CreateCategory(CategoryForCreationDto categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);
            await _categoryRepository.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryForUpdateDto categoryModel)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _mapper.Map(categoryModel, category);
            await _categoryRepository.UpdateCategoryAsync(category);

            return NoContent();
        }

    }
}
