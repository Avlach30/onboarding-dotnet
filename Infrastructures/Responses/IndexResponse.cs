using System.Net;

namespace onboarding_dotnet.Infrastructures.Responses;

public class IndexResponse<T>
{
    public required int Status { get; set; }

    public required string Message { get; set; }

    public required List<T> Data { get; set; }

    public required IndexResponseMetadata Meta { get; set; }

    public static IndexResponse<T> Success(
        List<T> data, 
        int total,
        string message = "Ok",
        int page = 1,
        int perPage = 10
    )
    {
        return new IndexResponse<T>
        {
            Status = (int)HttpStatusCode.OK,
            Message = message,
            Data = data,
            Meta = new IndexResponseMetadata(total, page, perPage)
        };
    }
}