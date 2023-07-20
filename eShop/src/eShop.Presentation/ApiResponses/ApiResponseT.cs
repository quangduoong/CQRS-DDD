using System.Net;

namespace eShop.Presentation.ApiResponses;

public class ApiResponse<TValue> : ApiResponse
{
    public TValue? Value { get; set; }

    public ApiResponse(HttpStatusCode statusCode, string message, TValue value)
        : base(statusCode, message)
    {
        Value = value;
    }
}