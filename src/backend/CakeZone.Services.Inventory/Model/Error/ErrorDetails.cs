using System.Text.Json;

namespace CakeZone.Services.Inventory.Model.Error
{
    public class ErrorDetails
    {
        public Guid ApiResponseId { get; set; }
        public string Instance { get; set; }
        public string? Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
