using CakeZone.Services.Inventory.Repository;
using Chronos.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Inventory.Controllers
{
    [Route("api/v1/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            var result = await _inventoryRepository.ListAllAsync();
            return ApiResponseExtension.ToPaginatedApiResult(result);
        }
    }
}
