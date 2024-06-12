using System.Net;

namespace OsDsII.api.Exceptions
{
    public class BadRequest : BaseException
    {

        public BadRequest(string message) :
        base
            (
                "HSO-003", // código identificador de erros  // 0 - 400 1 - 500
                message,
                HttpStatusCode.BadRequest,
                StatusCodes.Status400BadRequest,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

    }
}
