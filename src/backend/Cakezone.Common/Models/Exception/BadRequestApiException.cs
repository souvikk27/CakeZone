namespace CakeZone.Common.Models.Exception
{
    public class BadRequestApiException : System.Exception
    {
        private const string BadRequestMessage = "The request is invalid";

        public BadRequestApiException() : base(BadRequestMessage)
        {
        }

        public BadRequestApiException(string reason) : base($"{BadRequestMessage} Reason : {reason}")
        {
        }

        public BadRequestApiException(string reason, System.Exception innerException) : base($"{BadRequestMessage} Reason : {reason}", innerException)
        {
        }
    }
}
