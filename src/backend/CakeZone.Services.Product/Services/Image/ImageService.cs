using CakeZone.Services.Product.Services.Validation;
using FluentValidation;

namespace CakeZone.Services.Product.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var validator = new ImageFileValidator();
            var validationResult = await validator.ValidateAsync(imageFile);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/cake/");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/cake/" + uniqueFileName;
        }

        public async Task<bool> RemoveImageAsync(string filePath)
        {
            var uploadesFolder = _webHostEnvironment.WebRootPath + filePath;
            if (File.Exists(uploadesFolder))
            {
                await Task.Run(() => File.Delete(uploadesFolder));
                return true;
            }
            return false;
        }
    }
}