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
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid image file.");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/cake/");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
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