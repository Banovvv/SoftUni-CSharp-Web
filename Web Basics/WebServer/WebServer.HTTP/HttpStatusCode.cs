namespace WebServer.HTTP
{
    public enum HttpStatusCode
    {
        OK = 200,
        Created = 201,
        MovedPermanently = 301,
        Found = 302,
        TemporaryRedirect = 307,
        PermanentRedirect = 308,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        RequestTimeout = 408,
        TooManyRequests = 429,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        HTTPVersionNotSupported = 505
    }
}
