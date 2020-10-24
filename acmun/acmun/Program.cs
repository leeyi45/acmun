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

        static async Task Main()
        {
            var server = new HTTPServer(new[] { "http://localhost:8000/" }, @"E:\source\repos\acmun\acmun\acmun\WebRoot\");
            server.Start();

            await server.WaitClosed();
        }
    }
}
