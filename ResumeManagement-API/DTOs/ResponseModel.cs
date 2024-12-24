namespace ResumeManagement_API.DTOs
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }

        // Constructor for success response
        public ResponseModel(T data, string message = "", int statusCode = 200)
        {
            Success = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        // Constructor for error response
        public ResponseModel(string message, int statusCode = 400)
        {
            Success = false;
            Message = message;
            Data = default;
            StatusCode = statusCode;
        }
    }


}
