using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer.HTTP
{
    public class HttpServer : IHttpServer
    {
        private readonly IDictionary<string, Func<HttpRequest, HttpResponse>>
            _routeTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            if (_routeTable.ContainsKey(path))
            {
                _routeTable[path] = action;
            }
            else
            {
                _routeTable.Add(path, action);
            }
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);

            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                ProcessClientAsync(tcpClient);
            }
        }

        private static async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    int position = 0;
                    byte[] buffer = new byte[HttpConstants.BufferSize];
                    List<byte> data = new List<byte>();

                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, position, buffer.Length);

                        position += count;

                        if (count < buffer.Length)
                        {
                            var partialBuffer = new byte[count];
                            Array.Copy(buffer, partialBuffer, count);

                            data.AddRange(partialBuffer);

                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }
                    }

                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                    var request = new HttpRequest(requestAsString);

                    var responseHtml = $@"<h1>Welcome</h1>
                                         {request.Method} {request.Path} {request.Headers.Count} => headers";
                    var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

                    var response = new HttpResponse("text/html", responseBodyBytes);
                    response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
                    { HttpOnly = true, MaxAge = 3 * 24 * 60 * 60 });
                    var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                    await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);
                }

                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
