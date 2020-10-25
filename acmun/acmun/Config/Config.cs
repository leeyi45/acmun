using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace acmun.Config
{
    /// <summary>
    /// Class for web server configuration
    /// </summary>
    [Serializable]
    public class Config
    {
        public Config(string addressString, int port, string webRoot)
        {
            AddressString = addressString;
            Port = port;
            WebRoot = webRoot;
        }

        Config()
        {
            Address = GetIPAddress();
            AddressString = Address.ToString();
            Port = 80;
            WebRoot = null;
        }

        [XmlElement("Address", IsNullable = false)]
        public string AddressString
        {
            get => Address.ToString();
            set => Address = IPAddress.Parse(value);
        }

        [XmlIgnore]
        public IPAddress Address { get; set; }

        public int Port { get; set; }

        [XmlElement(IsNullable = false)]
        public string WebRoot { get; set; }

        public static Config DefaultConfig => new Config();

        /// <summary>
        /// Load server configuration information from the XML file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Config LoadConfig(string path)
            => HelperMethods.DeserializeXMLFromFile<Config>(path);

        public static void WriteConfig(string path, Config obj)
            => HelperMethods.SerializeToXMLFile(path, obj);

        /// <summary>
        /// Gets the first available IP address of an enabled network interface
        /// </summary>
        /// <returns>IP Address</returns>
        public static IPAddress GetIPAddress()
        {
            foreach (var iface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (iface.NetworkInterfaceType != NetworkInterfaceType.Wireless80211 &&
                    iface.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;

                return iface.GetIPProperties().UnicastAddresses.First().Address;
            }
            throw new ArgumentException("Could not find a suitable network interface");
        }

        static void RunNetsh(string args)
        {
            Console.WriteLine($"netsh {args}");
            var psi = new ProcessStartInfo("netsh", args)
            {
                Verb = "runas",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true
            };

            Process.Start(psi).WaitForExit();
        }

        /// <summary>
        /// Add a URL reservation to use HttpListener
        /// Only required with windows
        /// </summary>
        /// <param name="address">Address to bind to</param>
        /// <param name="port">Port to bind to</param>
        public static void AddWindowsUrl(string prefix)
        {
            //netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user
            var userName = Environment.UserName;
            var domainName = Environment.UserDomainName;
            RunNetsh($"http add urlacl url={prefix} user={domainName}\\{userName}");
        }

        public static void RemoveWindowsUrl(string prefix) 
            => RunNetsh($"http delete urlacl url={prefix}");
    }
}
