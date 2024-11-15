using System.Net;

namespace onboarding_dotnet.Infrastructures.Responses;

public class ApiResponse
{
    public int Status { get; set; }
    public required string Message { get; set; }

    public List<string>? Errors { get; set; }

    public static ApiResponse Success(string message = "Ok")
    {
        return new ApiResponse
        {
            Status = (int)HttpStatusCode.OK,
            Message = message
        };
    }

    public static ApiResponse Fail(string message, int statusCode = (int)HttpStatusCode.InternalServerError, List<string>? errors = null)
    {
        return new ApiResponse
        {
            Status = statusCode == 0 ? (int)HttpStatusCode.InternalServerError : statusCode,
            Message = message,
            Errors = errors
        };
    }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string message = "Ok")
    {
        return new ApiResponse<T>
        {
            Status = (int)HttpStatusCode.OK,
            Message = message,
            Data = data
        };
    }
}