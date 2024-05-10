using System.Text.Json;

namespace CakeZone.Services.Product.Model.Error
{
    public class ErrorDetails
    {
        public int SattusCode { get; set; }
        public string? Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
