namespace Papara.Base.Response
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime ServerDate { get; set; } = DateTime.UtcNow;
        public Guid ReferenceNumber { get; set; } = Guid.NewGuid();

        public BaseResponse()
        {
            IsSuccess = true;
            Message = "Success";
        }

        public BaseResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public BaseResponse(T data, string message = "Success")
            : base(true, message)
        {
            Data = data;
        }

        public BaseResponse(string message)
            : base(false, message)
        {
            Data = default;
        }
    }

    public class BaseErrorResponse : BaseResponse
    {
        public List<string> Errors { get; set; } = new List<string>();

        public BaseErrorResponse(string message)
            : base(false, message)
        {
        }

        public BaseErrorResponse(string message, List<string> errors)
            : base(false, message)
        {
            Errors = errors;
        }

        public BaseErrorResponse(List<string> errors)
            : base(false, "Error")
        {
            Errors = errors;
        }
    }
}
