namespace CakeZone.Services.Product.Model.Exceptions
{
    public class BadRequestApiException : Exception
    {
        private const string BadRequestMessage = "The request is invalid";

        public BadRequestApiException() : base(BadRequestMessage)
        {
        }

        public BadRequestApiException(string reason) : base($"{BadRequestMessage} Reason : {reason}")
        {
        }

        public BadRequestApiException(string reason, Exception innerException) : base($"{BadRequestMessage} Reason : {reason}", innerException)
        {
        }
    }
}
