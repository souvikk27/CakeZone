namespace CakeZone.Services.Inventory.Model.Error;

public enum HttpStatusType
{
    Informational, // 1xx
    Success,       // 2xx
    Redirection,   // 3xx
    ClientError,   // 4xx
    ServerError,   // 5xx
    Unknown        // for any other status codes
}