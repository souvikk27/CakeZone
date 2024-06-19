using System.Text.Json;

namespace CakeZone.Services.Inventory.Model.Error
{
    public class ErrorDetails
    {
        public string? Instance { get; set; }
        public int SattusCode { get; set; }
        public string? Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
