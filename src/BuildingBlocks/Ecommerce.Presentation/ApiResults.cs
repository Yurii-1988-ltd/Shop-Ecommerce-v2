

using Ecommerce.Domain.Domain;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Presentation;

public static class ApiResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }
        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Type),
            statusCode: GetStatusCode(result.Error.Type),
            extensions: GetErrors(result));

    }

    private static IEnumerable<KeyValuePair<string, object?>>? GetErrors(Result result)
    {
        throw new NotImplementedException();
    }

    private static int? GetStatusCode(ErrorType type)
    => type switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Problem => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status500InternalServerError
    };

    static string GetType(ErrorType errorType) =>
          errorType switch
          {
              ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
              ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
              ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
              ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
              _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
          };



    private static string? GetDetail(Error error)
        => error.Type switch
        {
            ErrorType.Validation => error.Description,
            ErrorType.NotFound => error.Description,
            ErrorType.Conflict => error.Description,
            ErrorType.Problem => error.Description,
            _ => "An unexpected error occurred."
        };


    private static string? GetTitle(Error error)
    => error.Type switch
    {
        ErrorType.Validation => error.Code,
        ErrorType.NotFound => error.Code,
        ErrorType.Conflict => error.Code,
        ErrorType.Problem => error.Code,
        _ => "Server failure"
    };
}
