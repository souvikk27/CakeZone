using AutoMapper;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Attribute;
using CakeZone.Services.Product.Services.Logging;
using CakeZone.Services.Product.Shared.Attributes;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}