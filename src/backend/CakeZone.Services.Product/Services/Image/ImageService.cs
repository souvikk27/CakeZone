namespace CakeZone.Services.Product.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveImageAsync(string imagePath)
        {

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/cake/");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imagePath);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Copy the image file
            await Task.Run(() => System.IO.File.Create(filePath));

            return "/images/cake/" + uniqueFileName;
        }
    }
}
