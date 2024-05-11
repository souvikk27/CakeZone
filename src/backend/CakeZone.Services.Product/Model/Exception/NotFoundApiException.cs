namespace CakeZone.Services.Product.Model.Exception
{
    public class NotFoundApiException : System.Exception
    {
        private const string NotFoundMessage = "The requested resource was not found.";

        public NotFoundApiException() : base(NotFoundMessage)
        {
        }

        public NotFoundApiException(string resourceName) : base($"{NotFoundMessage} {resourceName}")
        {
        }

        public NotFoundApiException(string resourceName, System.Exception innerException) : base($"{NotFoundMessage} {resourceName}", innerException)
        {
        }
    }
}
