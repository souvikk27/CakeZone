using CakeZone.Services.Product.CQRS.Attribute;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Services.Filters;
using CakeZone.Services.Product.Shared.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Controllers
{
    [Route("api/v1/attributes")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttributeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAttributes([FromQuery] AttributeParameter attributeParameter)
        {
            var query = new GetAllAttributesQuery(attributeParameter);
            var attributes = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(attributes,
                    "attributes",
                    "200",
                    attributes.MetaData.CurrentPage,
                    attributes.MetaData.TotalPages);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAttributeById(Guid id)
        {
            var query = new GetAttributeByIdQuery(id);
            var attribute = await _mediator.Send(query);
            return ApiResponseExtension.ToSuccessApiResult(attribute,
                    "attribute");
        }

        [HttpGet("attribute")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAttributeByName([FromQuery] string name)
        {
            var query = new GetAttributeByNameQuery(name);
            var attribute = await _mediator.Send(query);
            return attribute == null
                ? ApiResponseExtension.ToErrorApiResult("Not Found",
                    "Requested attribute doesn't exist",
                    "404")
                : ApiResponseExtension.ToSuccessApiResult(attribute,
                    "attribute");
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAttribute([FromBody] CreateAttributeDto createAttribute)
        {
            var command = new CreateAttributeCommand(createAttribute);
            var attribute = await _mediator.Send(command);
            if (attribute == null)
            {
                return ApiResponseExtension.ToErrorApiResult("Bad Request",
                    $"Attribute with name {createAttribute.AttributeName} " +
                    $"already exists either change attribute name or contact support!",
                    "400");
            }
            return ApiResponseExtension.ToSuccessApiResult(attribute, "attribute created", "200");
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAttribute([FromBody] UpdateAttributeDto updateAttribute)
        {
            var command = new UpdateAttributeCommand(updateAttribute);
            var attribute = await _mediator.Send(command);
            if (attribute == null) 
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found",
                    $"Attribute with name {updateAttribute.AttributeName} " +   
                    $"not found!", "404");
            }   
            return ApiResponseExtension.ToSuccessApiResult(attribute, "attribute updated", "204");  
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAttribute(Guid id)
        {
            var command = new DeleteAttributeCommand(id);
            var attribute = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(attribute, "attribute removed", "200");
        }
    }
}