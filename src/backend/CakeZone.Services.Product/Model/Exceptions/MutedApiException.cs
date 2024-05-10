namespace CakeZone.Services.Product.Model.Exceptions
{
    public class MutedApiException : Exception
    {
        private const string MutedMessage = "An error was encountered. Please contact support.";

        public MutedApiException() : base(MutedMessage)
        {
        }

        public MutedApiException(Exception innerException) : base(MutedMessage, innerException)
        {
        }
    }
}
