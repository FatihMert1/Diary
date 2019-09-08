namespace Diary.Api.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool Error { get; set; }

        public ApiResponse(T data, string message, int statusCode, bool error)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
            Error = error;
        }

        public ApiResponse()
        {
            
        }
    }
}