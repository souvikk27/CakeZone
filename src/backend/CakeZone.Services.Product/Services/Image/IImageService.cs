namespace CakeZone.Services.Product.Services.Image
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
        Task<bool> RemoveImageAsync(string filePath);
    }
}
