using AutoMapper;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Attribute;
using CakeZone.Services.Product.Services.Logging;
using CakeZone.Services.Product.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Controllers
{
    [Route("api/v1/attributes")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IAttributeRepository _attributeRepository;

        public AttributeController(ILoggerManager logger, IAttributeRepository attributeRepository, IMapper mapper)
        {
            _logger = logger;
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributes()
        {
            var attributes = await _attributeRepository.GetAll();
            return Ok(attributes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeById(Guid id)
        {
            var attribute = await _attributeRepository.GetById(id);
            return ApiResponseExtension.ToSuccessApiResult(attribute, "attribute", "200");
        }

        [HttpGet("attribute/{name}")]
        public async Task<IActionResult> GetAttributeByName(string name)
        {
            var attribute = await _attributeRepository.FindAsync(a => a.AttributeName == name);
            return ApiResponseExtension.ToSuccessApiResult(attribute, "attribute", "200");
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
            var attribute = _mapper.Map<Model.Attribute>(createAttribute);
            await _attributeRepository.AddAsync(attribute);
            await _attributeRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(attribute, "attribute created", "200");
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAttribute([FromBody] UpdateAttributeDto updateAttribute)
        {
            var attribute = await _attributeRepository.GetById(updateAttribute.AttributeId);
            attribute = _mapper.Map(updateAttribute, attribute);
            await _attributeRepository.UpdateAsync(attribute);
            await _attributeRepository.SaveAsync();
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
            var attribute = await _attributeRepository.GetById(id);
            await _attributeRepository.Remove(attribute);
            await _attributeRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(attribute, "attribute removed", "200");
        }
    }
}