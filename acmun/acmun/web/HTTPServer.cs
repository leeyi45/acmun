using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using Windows.System;
using System.Runtime.InteropServices;
using Windows.UI.Xaml.Controls.Primitives;

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

        protected async void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                var resource = request.RawUrl.TrimStart('/');
                var resourcePath = Path.Combine(WebRoot, resource);

                if (!File.Exists(resourcePath))
                {
                    response.StatusCode = 404;
                    return;
                }

                var fileData = await File.ReadAllTextAsync(resourcePath, cancelSource.Token);
                using var stream = new StreamWriter(response.OutputStream);
                await stream.WriteAsync(fileData);
                await stream.FlushAsync();
            }
            catch
            {
                response.StatusCode = 500;
            }
            finally
            {
                response.Close();
            }
        }
    }
}
