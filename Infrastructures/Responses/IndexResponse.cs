using System.Net;

namespace onboarding_dotnet.Infrastructures.Responses;

public class IndexResponse<T>
{
    public required int Status { get; set; }

    public required string Message { get; set; }

    public required List<T> Data { get; set; }

    public static IndexResponse<T> Success(List<T> data, string message = "Ok")
    {
        return new IndexResponse<T>
        {
            Status = (int)HttpStatusCode.OK,
            Message = message,
            Data = data
        };
    }
}