namespace talabat.Apis.Errors
{
    public class ApiExeptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExeptionResponse(int statuscode , string? message = null , string? details = null):base(statuscode , message)
        {
            Details = details;
        }
    }
}
