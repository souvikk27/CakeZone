namespace CakeZone.Services.Inventory.Model.Error;

public static class HttpStatus
{
    public static HttpStatusType GetHttpStatusType(int statusCode)
    {
        return statusCode switch
        {
            >= 100 and < 200 => HttpStatusType.Informational,
            >= 200 and < 300 => HttpStatusType.Success,
            >= 300 and < 400 => HttpStatusType.Redirection,
            >= 400 and < 500 => HttpStatusType.ClientError,
            >= 500 and < 600 => HttpStatusType.ServerError,
            _ => HttpStatusType.Unknown
        };
    }
}