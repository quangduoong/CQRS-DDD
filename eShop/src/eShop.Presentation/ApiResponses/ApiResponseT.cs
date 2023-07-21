using System.Net;

namespace eShop.Presentation.ApiResponses;

public class ApiResponse<TValue> : ApiResponse
{
    public TValue? Payload { get; set; }

    public ApiResponse(HttpStatusCode statusCode, string message, TValue payload)
        : base(statusCode, message)
    {
        Payload = payload;
    }
}