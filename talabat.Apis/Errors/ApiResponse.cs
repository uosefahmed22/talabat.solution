namespace talabat.Apis.Errors
{
    public class ApiResponse
    {
        public int StatuseCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse (int statuseCode , string? message = null)
        {
            StatuseCode = statuseCode;
            Message = message ?? GetDefaultMessgaeForStatuseCode(StatuseCode);
        }

        private string? GetDefaultMessgaeForStatuseCode(int statuseCode)
        {
            return statuseCode switch
            {
                400 => "BadRequest",
                401 => "You are not Authorized",
                404 => "Resource Not Found",
                500=>"internal Server Error",
                _ => null ,
            };
        }
    }
}
