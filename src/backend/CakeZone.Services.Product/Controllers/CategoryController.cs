using CakeZone.Services.Product.CQRS.Category;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Services.Filters;
using CakeZone.Services.Product.Shared.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParameter categoryParameter)
        {
            var query = new GetCategoriesQuery(categoryParameter);
            var categories = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(categories,
                "categories",
                "200",
                categories.MetaData.CurrentPage,
                categories.MetaData.TotalPages);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var query = new GetCategoryByIdQuery(id);
            var category = await _mediator.Send(query);
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
            var query = new GetCategoryByNameQuery(categoryName);
            var category = await _mediator.Send(query);
            return category == null
                ? ApiResponseExtension.ToErrorApiResult("Not Found",
                    "requested category not found",
                    "404")
                : ApiResponseExtension.ToSuccessApiResult(category,
                    "category",
                    "200");
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
            var command = new CreateCategoryCommand(categoryDto);
            var category = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(category, "category created", "200");
        }

        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDto categoryDto)
        {
            var command = new UpdateCategoryCommand(categoryDto);
            var category = await _mediator.Send(command);
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
            var command = new DeleteCategoryCommand(id);
            var category = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(category, "category deleted", "200");
        }
    }
}