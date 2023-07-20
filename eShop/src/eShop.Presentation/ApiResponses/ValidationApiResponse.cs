using eShop.Domain.Shared;
using System.Net;

namespace eShop.Presentation.ApiResponses;

public class ValidationApiResponse : ApiResponse
{
    public IEnumerable<Error> Errors { get; }

    public ValidationApiResponse(IEnumerable<Error> errors) : base(HttpStatusCode.BadRequest, "One or more validation failures occurred.")
    {
        Errors = errors;
    }
}

