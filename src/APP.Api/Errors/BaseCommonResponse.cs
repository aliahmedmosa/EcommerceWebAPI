namespace APP.Api.Errors
{
    public class BaseCommonResponse
    {
        public BaseCommonResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefultMessageForStatusCode(statusCode);
        }

        private string DefultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad requiest",
                401 => "Not authorized",
                404 => "Resource not found",
                500 => "Server error",
                _ => null
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
