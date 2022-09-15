using HTTPProtocolDemo.Scrapers;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HTTPProtocolDemo
{
    public class Program
    {
        private const string _newline = "\r\n";
        public static async Task Main()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 8585);

            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                await ProccessRequestAsync(tcpClient);
            }
        }

        private static async Task ProccessRequestAsync(TcpClient tcpClient)
        {
            using (NetworkStream networkStream = tcpClient.GetStream())
            {
                byte[] requestBytes = new byte[1000000];
                int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
                string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);

                string responseText = @"<form action='/Account/Login' method='post'> 
                                            <input type=text name='username' /> 
                                            <input type=password name='password' /> 
                                            <input type=date name='date' /> 
                                            <input type=submit value='login' /> 
                                            </form>";

                var response = "HTTP/1.0 200 OK" +
                               _newline +
                               "Server: SoftUniServer/1.0" +
                               _newline +
                               "Content-Type: text/html" +
                               _newline +
                               $"Set-Cookie: sid={Guid.NewGuid()}" +
                               _newline +
                               $"Content-Length: {responseText.Length}" +
                               _newline +
                               _newline +
                               responseText;

                var responseBytes = Encoding.UTF8.GetBytes(response);

                await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);

                Console.WriteLine(request);
                Console.WriteLine(new string('*', 50));
            }
        }
    }
}
