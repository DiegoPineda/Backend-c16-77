using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.BrandDto;
using Ecommerce.Services.BrandService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandController(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _brandRepository.GetAllBrandsAsync();
            var responseDto = _mapper.Map<List<BrandDto>>(brands);

            return Ok(responseDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandById(int id)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            var responseDto = _mapper.Map<BrandDto>(brand);
            return Ok(responseDto);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Brand>> CreateBrand(BrandForCreationDto brandModel)
        {
            var brand = _mapper.Map<Brand>(brandModel);
            await _brandRepository.AddBrandAsync(brand);
            return CreatedAtAction(nameof(GetBrandById), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateBrand(int id, BrandForUpdateDto brandModel)
        {
            var brand = await _brandRepository.GetBrandByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _mapper.Map(brandModel, brand);
            await _brandRepository.UpdateBrandAsync(brand);

            return NoContent();
        }
    }
}
