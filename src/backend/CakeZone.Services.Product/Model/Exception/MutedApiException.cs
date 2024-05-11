namespace CakeZone.Services.Product.Model.Exception
{
    public class MutedApiException : System.Exception
    {
        private const string MutedMessage = "An error was encountered. Please contact support.";

        public MutedApiException() : base(MutedMessage)
        {
        }

        public MutedApiException(System.Exception innerException) : base(MutedMessage, innerException)
        {
        }
    }
}
