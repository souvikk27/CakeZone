namespace CakeZone.Services.Product.Model.Generic
{
    public class PaginatedResponseModel<T>
    {
        public Guid ApiResponseId { get; set; }

        public string Status { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public T Payload { get; set; }

        //Generate constructor with patameters
        public PaginatedResponseModel(ApiResponseStatusEnum status, string message, T payload, 
            string? statusCode = null, Guid? apiResponseId = null, 
            int currentPage = 0, int totalPages = 0)
        {
            ApiResponseId = apiResponseId ?? Guid.NewGuid();
            Status = Enum.GetName(typeof(ApiResponseStatusEnum), status)?.ToLower();
            StatusCode = statusCode != null ? Convert.ToInt32(statusCode) : GetStatusCode(status);
            Message = message;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            Payload = payload;
        }

        private int GetStatusCode(ApiResponseStatusEnum status)
        {
            int statusCode = 0;
            switch (status)
            {
                case ApiResponseStatusEnum.Success:
                    statusCode = 200;
                    break;
                case ApiResponseStatusEnum.Warning:
                    statusCode = 429;
                    break;
                case ApiResponseStatusEnum.Error:
                    statusCode = 400;
                    break;
                default:
                    statusCode = 100;
                    break;
            }
            return statusCode;
        }
    }
}
