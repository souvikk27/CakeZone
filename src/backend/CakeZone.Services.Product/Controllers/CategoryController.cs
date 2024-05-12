using AutoMapper;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Category;
using CakeZone.Services.Product.Shared.Categories;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Controllers
{
    [Route("api/v1/categories")]
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
        
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Model.Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(category, "catedory created", "200");
        }
        
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryDto)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found","Requested category not found", "404");
            }
            category = _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(category, "catedory updated", "204");
        }

        
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCategory(Guid id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found","Requested category not found", "404");
            }
            await _categoryRepository.Remove(category);
            await _categoryRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(category, "catedory deleted", "200");
        }
    }
}
