using CakeZone.Services.Inventory.Shared.Inventory;
using FluentValidation;

namespace CakeZone.Services.Inventory.Services.Validation
{
    public class CreateInventoryValidator : AbstractValidator<CreateInventoryDto>
    {
        public CreateInventoryValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty()
                .WithMessage("Product Id cannot be empty");

            RuleFor(x => x.StorageDepotId).NotEmpty()
                .WithMessage("Storage Depot Id cannot be empty");

            RuleFor(x => x.CurrentLevel).NotEmpty()
                .WithMessage("Current Level cannot be empty");

            RuleFor(x => x.MinLevel).NotEmpty()
                .WithMessage("Minimum Level cannot be empty");

            RuleFor(x => x.Demand).NotEmpty()
                .WithMessage("Demand cannot be empty");

            RuleFor(x => x.HoldingCostPerUnit).NotEmpty()
                .WithMessage("Holding Cost per unit cannot be empty");
        }
    }
}
