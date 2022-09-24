namespace WebServer.HTTP
{
    public enum HttpStatusCode
    {
        OK = 200,
        Created = 201,
        MovedPermanently = 301,
        Found = 302,
        TemporaryRedirect = 307,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500,
        NotImplemented = 501
    }
}
