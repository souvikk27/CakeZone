using FluentValidation;
using SixLabors.ImageSharp.PixelFormats;

namespace CakeZone.Services.Product.Services.Validation
{
    public class ImageFileValidator : AbstractValidator<IFormFile>
    {
        private readonly string[] _validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        private const long MaxFileSize = 1024 * 1024;
        private const int MinWidth = 1200;
        private const int MinHeight = 1200;
        private const int MaxWidth = 1200;
        private const int MaxHeight = 1200;

        public ImageFileValidator()
        {
            RuleFor(file => file)
            .NotNull()
            .WithMessage("Image file is required.");

            RuleFor(file => file.Length)
            .GreaterThan(0)
            .WithMessage("Image file cannot be empty.");

            RuleFor(file => Path.GetExtension(file.FileName).ToLowerInvariant())
            .Must(ext => _validExtensions.Contains(ext))
            .WithMessage("Invalid image file extension.");

            RuleFor(file => file.Length)
            .LessThanOrEqualTo(MaxFileSize)
            .WithMessage($"Image file size cannot exceed {MaxFileSize / (1024 * 1024)} MB.");

            RuleFor(file => file)
            .Must(HaveValidResolution)
            .WithMessage($"Image resolution must be between {MinWidth}x{MinHeight} and {MaxWidth}x{MaxHeight} pixels.");
        }

        private bool HaveValidResolution(IFormFile file)
        {
            using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(file.OpenReadStream());
            return image.Width >= MinWidth && image.Height >= MinHeight && image.Width <= MaxWidth && image.Height <= MaxHeight;
        }
    }
}
