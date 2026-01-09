namespace SureLbraryAPI.Utilities
{
    public class ResponseDetails<T>
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public int StatusCode { get; set; }
        
        public T? Data { get; set; }

        public static ResponseDetails<T> Success(T data, string message = "", int statusCode = 200)
        {
            var response = new ResponseDetails<T>
            {
                Message = message,
                IsSuccess = true,
                StatusCode = statusCode,
                Data = data
            };
            return response;
        }
        public static ResponseDetails<T> Failed(string message = "", string error = "", int statusCode = 400)
        {
            var response = new ResponseDetails<T>
            {
                Message = message,
                IsSuccess = false,
                StatusCode = statusCode,
                Error = error
            };
            return response;
        }
    }
}