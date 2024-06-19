namespace CakeZone.Services.Inventory.Model.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public const string message = "Unauthorized access to resource";

        public UnauthorizedException() : base(message)
        {
        }

        public UnauthorizedException(string resourceName) : base($"{message} {resourceName}")
        {
        }

        public UnauthorizedException(string resourceName, Exception innerException) : base($"{message} {resourceName}", innerException)
        {
        }
    }
}
