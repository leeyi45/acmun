using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Text;

namespace acmun
{
    class HTTPServer
    {
        public HTTPServer(string[] prefixes, string webRoot)
        {
            server = new HttpListener();

            foreach (var entry in prefixes) server.Prefixes.Add(entry);

            closeTcs = new TaskCompletionSource<Exception>();
            cancelSource = new CancellationTokenSource();
            WebRoot = webRoot;
        }

        readonly HttpListener server;
        readonly TaskCompletionSource<Exception> closeTcs;
        readonly CancellationTokenSource cancelSource;

        public string WebRoot { get; }

        public string LandingPage { get; } = "index.html";

        public string Error404Page { get; } = null;

        public void Start()
        {
            server.Start();
            server.BeginGetContext(RequestHandler, server);
        }

        public void Stop()
        {
            if (cancelSource.IsCancellationRequested) return;
            cancelSource.Cancel();
            server.Stop();
        }

        public async Task WaitClosed() => await closeTcs.Task;

        void RequestHandler(IAsyncResult result)
        {
            var server = (HttpListener)result.AsyncState;
            var context = server.EndGetContext(result);

            server.BeginGetContext(RequestHandler, server);

            HandleRequest(context.Request, context.Response);
        }

        static string GetStatusDescription(int code) => code switch
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

        protected async void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                //Console.WriteLine(request.Cookies["sessid"].Value);
                //if (request.Cookies["sessid"] == null)
                //{ //User's not logged in
                //    response.Redirect("loginTest.html");
                //    return;
                //}

                if (request.RawUrl == "/")
                { //Default landing page
                    response.Redirect(LandingPage);
                    return;
                }

                var resource = request.RawUrl.TrimStart('/');

                var resourcePath = Path.Combine(WebRoot, resource);
                //var fullPath = Path.GetFullPath(resourcePath);

                //Console.WriteLine(Path.GetDirectoryName(fullPath));
                //Console.WriteLine($"Request for {fullPath} received");

                if(!File.Exists(resourcePath))// || Path.GetDirectoryName(fullPath) != WebRoot)
                {
                    //Unknown resource was requested
                    if (Error404Page != null) response.Redirect(Error404Page);
                    else
                    {
                        response.StatusCode = 404;
                        response.StatusDescription = GetStatusDescription(404);
                    }
                    return;
                }

                using var fileStream = File.OpenRead(resourcePath);
                response.ContentType = "text/html";

                await fileStream.CopyToAsync(response.OutputStream, cancelSource.Token);
                await response.OutputStream.FlushAsync();
            }
            catch
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.StatusDescription = GetStatusDescription(500);
            }
            finally
            {
                response.Close();
            }
        }
    }
}
