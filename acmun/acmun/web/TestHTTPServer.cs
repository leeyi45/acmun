using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace acmun.TestHTTP
{
    class TestHTTPServer
    {
        public TestHTTPServer(IPAddress address, int port)
        {
            serverThread = new Thread(ServerLoop);
            cancelSource = new CancellationTokenSource();
            serverCloseFuture = new TaskCompletionSource<Exception>();
            listener = new TcpListener(address, port);
        }

        readonly Thread serverThread;
        readonly CancellationTokenSource cancelSource;
        readonly TaskCompletionSource<Exception> serverCloseFuture;
        readonly TcpListener listener;

        public void Start()
        {
            listener.Start();
            serverThread.Start();
        }

        public void Stop()
        {
            if (cancelSource.IsCancellationRequested) return;
            cancelSource.Cancel();
        }

        void OnClientConnect(TcpClient client)
        {
            using var reader = new StreamReader(client.GetStream());
            using var writer = new StreamWriter(client.GetStream());
            using var stream = client.GetStream();
            stream.ReadTimeout = 100;
            var lines = new List<string>();

            try
            {
                while(true)
                {
                    string line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line)) break;
                    lines.Add(line);
                }
            }
            catch (IOException) { }

            var request = HTTPRequest.Parse(lines.ToArray());

            Console.WriteLine($"Method is {request.Method}");
            foreach (var (key, value) in request.Headers)
            {
                Console.WriteLine($"  {key}: {value}");
            }

            //Console.WriteLine(input.ToString());

            writer.Write("HTTP/ 1.1 200 OK\r\nContent-Type: text/html\r\n\r\n");
            writer.Write(File.ReadAllText(@"E:\source\repos\acmun\acmun\acmun\index.html"));
            writer.Flush();

            //ClientConnected?.Invoke(reader, write);
        }

        public event ClientConnectEvent ClientConnected;

        void ServerLoop()
        {
            try
            {
                while (!cancelSource.IsCancellationRequested)
                {
                    var acceptTask = listener.AcceptTcpClientAsync();
                    acceptTask.Wait(cancelSource.Token);
                    var client = acceptTask.Result;

                    Console.WriteLine($"{client.Client.RemoteEndPoint} connected!");
                    OnClientConnect(client);
                }
                serverCloseFuture.SetResult(null);
            }
            catch (Exception e)
            {
                serverCloseFuture.SetResult(e);
            }
            finally
            {
                Stop();
            }
        }

        public Task<Exception> CloseFuture => serverCloseFuture.Task;
    }

    class HTTPRequest
    {
        public HTTPRequest(HTTPMethod method, string uRI, Dictionary<string, string> headers, string content)
        {
            Method = method;
            URI = uRI;
            Headers = headers;
            Content = content;
        }

        //  GET /favicon.ico HTTP/1.1
        //Host: 127.0.0.1
        //Connection: keep-alive
        //Pragma: no-cache
        //Cache-Control: no-cache
        //User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36
        //DNT: 1
        //Accept: image/avif,image/webp,image/apng,image/*,*/*;q=0.8
        //Sec-Fetch-Site: same-origin
        //Sec-Fetch-Mode: no-cors
        //Sec-Fetch-Dest: image
        //Referer: http://127.0.0.1/
        //Accept-Encoding: gzip, deflate, br
        //Accept-Language: en-US,en;q=0.9
        //Cookie: isNotIncognito=true; _ga=GA1.1.915662125.1590887152

        public HTTPMethod Method { get; }
        public string URI { get; }
        public Dictionary<string, string> Headers { get; }
        public string Content { get; }

        public static HTTPRequest Parse(string[] lines)
        {
            #region Step 1 Line 1
            string method, uri;
            {
                string[] args = lines[0].Split(' ');
                method = args[0];
                uri = args[1].TrimStart('/');
            }
            #endregion

            var headers = new Dictionary<string, string>();

            int i = 1;
            for (; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) break;

                var match = Regex.Match(lines[i], "^(.+?): (.+)$");
                headers.Add(match.Groups[1].Value, match.Groups[2].Value);
            }

            return new HTTPRequest((HTTPMethod)Enum.Parse(typeof(HTTPMethod), method), uri, headers, string.Join('\n', lines[(i + 1)..]));
        }
    }

    class HTTPResponse
    {
        public HTTPResponse(int code, Dictionary<string, string> headers, string content)
        {
            Code = code;
            Msg = GetMessage(code);
            Headers = headers;
            Content = content;
        }

        static string GetMessage(int code) => code switch
        {
            200 => "Ok",
            201 => "Created",
            202 => "Accepted",
            204 => "No Content",

            301 => "Moved Permanently",
            302 => "Redirection",
            304 => "Not Modified",

            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",

            500 => "Internal Server Error",
            501 => "Not Implemented",
            502 => "Bad Gateway",
            503 => "Service Unavailable",
            _ => throw new ArgumentException($"Unknown code: {code}")
        };

        public int Code { get; }
        public string Msg { get; }

        public Dictionary<string, string> Headers { get; }
        public string Content { get; }

        public override string ToString()
        {
            //writer.Write("HTTP/ 1.1 200 OK\r\nContent-Type: text/html\r\n\r\n")
            var output = new StringBuilder($"HTTP/ 1.1 {Code} {Msg}\r\n");

            foreach (var (key, value) in Headers)
            {
                output.Append($"{key}: {value}\r\n");
            }

            output.Append(Content);
            return output.ToString();
        }
    }

    enum HTTPMethod
    {
        None, GET, POST
    }

    delegate Task ClientConnectEvent(StreamReader reader, StreamWriter writer);
}
