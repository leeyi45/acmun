using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.UI.Xaml;

namespace acmun
{
    class Program
    {
        static Config.Config CurrentConfig;

        static async Task Main()
        {
            if (!HttpListener.IsSupported) return;

            try
            {
                CurrentConfig = Config.Config.LoadConfig(@"E:\source\repos\acmun\acmun\acmun\Config\config.xml");
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Could not locate configuration xml!");
                return;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Could not load configuration xml: {e.Message}");
                return;
            }

            var prefix = $"http://{CurrentConfig.AddressString}:{CurrentConfig.Port}/";

            //var server = new HTTPServer(new[] { prefix }, @"E:\source\repos\acmun\acmun\acmun\WebRoot\");
            while(true)
            {
                var server = new HTTPServer(new[] { prefix }, CurrentConfig.WebRoot);
                try
                {
                    server.Start();
                    await server.WaitClosed();
                    break;
                }
                catch(HttpListenerException e)
                {
                    if (e.Message == "Access is denied.") Config.Config.AddWindowsUrl(prefix);
                }
            }
        }
    }
}
