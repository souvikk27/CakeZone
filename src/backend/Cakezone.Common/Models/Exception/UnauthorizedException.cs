namespace CakeZone.Common.Models.Exception
{
    public class UnauthorizedException : System.Exception
    {
        public const string message = "Unauthorized access to resource";

        public UnauthorizedException() : base(message)
        {
        }

        public UnauthorizedException(string resourceName) : base($"{message} {resourceName}")
        {
        }

        public UnauthorizedException(string resourceName, System.Exception innerException) : base($"{message} {resourceName}", innerException)
        {
        }
    }
}
