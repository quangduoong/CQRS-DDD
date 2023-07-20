using System.Net;

namespace eShop.Presentation.ApiResponses;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; init; }

    public string Message { get; init; } = string.Empty;

    public ApiResponse(HttpStatusCode statusCode, string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new Exception("Message cannot be null");

        StatusCode = statusCode;
        Message = message;
    }

    public static ApiResponse<TValue> Ok<TValue>(TValue value)
        => new(HttpStatusCode.OK, "Data fetched.", value);

    public static ApiResponse<TValue> Created<TValue>(TValue value)
        => new(HttpStatusCode.Created, "A new instance was added to the database.", value);

    public static ApiResponse NotFound(string errorMessage)
        => new(HttpStatusCode.NotFound, errorMessage);

    public static ApiResponse BadRequest(string errorMessage)
        => new(HttpStatusCode.BadRequest, errorMessage);
}

