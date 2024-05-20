using AutoMapper;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Model;
using CakeZone.Services.Product.Repository.Category;
using CakeZone.Services.Product.Services;
using CakeZone.Services.Product.Services.FIlters;
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

        /// <inheritdoc />
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParameter categoryParameter)
        {
            var categories= await _categoryRepository.GetAll();
            var filteredcategory = categories.Where(category =>
                                     (categoryParameter.AddedOn == DateTime.MinValue || categoryParameter.AddedOn == category.CreatedAt) &&
                                     (string.IsNullOrEmpty(categoryParameter.CategoryName) || categoryParameter.CategoryName == category.Name))
                                     .ToList();

            var metadata = new MetaData().Initialize(categoryParameter.PageNumber, categoryParameter.PageSize, filteredcategory.Count());
            metadata.AddResponseHeaders(Response);
            var pagedList = PagedList<Category>.ToPagedList(filteredcategory, categoryParameter.PageNumber, categoryParameter.PageSize);
            return Ok(pagedList);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetById(id);
            return ApiResponseExtension.ToSuccessApiResult(category, "category", "200");
        }


        [HttpGet("category")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryByName([FromQuery] string categoryName)
        {
            var category = await _categoryRepository.FindAsync(c => c.Name == categoryName);
            if (category.Count() == 0)
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found", "requuested category not found", "404");
            }
            return ApiResponseExtension.ToSuccessApiResult(category, "category", "200");
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
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(category, "category created", "200");
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto categoryDto)
        {
            var category = await _categoryRepository.GetById(categoryDto.CategoryId);
            if (category == null)
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found", "Requested category not found", "404");
            }
            category = _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(category, "category updated", "204");
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
                return ApiResponseExtension.ToErrorApiResult("Not Found", "Requested category not found", "404");
            }
            await _categoryRepository.Remove(category);
            await _categoryRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(category, "category deleted", "200");
        }
    }
}