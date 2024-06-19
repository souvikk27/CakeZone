namespace CakeZone.Services.Inventory.Model.Exceptions
{
    public class NotFoundApiException : Exception
    {
        private const string NotFoundMessage = "The requested resource was not found.";

        public NotFoundApiException() : base(NotFoundMessage)
        {
        }

        public NotFoundApiException(string resourceName) : base($"{NotFoundMessage} {resourceName}")
        {
        }

        public NotFoundApiException(string resourceName, Exception innerException) : base($"{NotFoundMessage} {resourceName}", innerException)
        {
        }
    }
}
