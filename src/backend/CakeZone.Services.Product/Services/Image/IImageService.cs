namespace CakeZone.Services.Product.Services.Image
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(string imageFile);
    }
}
