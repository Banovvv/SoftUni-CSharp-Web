using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WebServer.HTTP
{
    public class HttpServer : IHttpServer
    {
        private IDictionary<string, Func<HttpRequest, HttpResponse>>
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

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using (NetworkStream stream = tcpClient.GetStream())
            {
                int position = 0;
                byte[] buffer = new byte[4096];
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
            }
        }
    }
}
