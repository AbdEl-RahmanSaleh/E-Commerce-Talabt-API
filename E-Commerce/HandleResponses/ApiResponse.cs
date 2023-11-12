namespace E_Commerce.HandleResponses
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatesCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatesCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int code)
            //MatchPattern
            => code switch
            {
                400 => "Bad Request",
                401 => "You are not Authorized!!",
                404 => "Resource not Found",
                500 => "Internal Server Error",
                _ => null
            };


    }
}
