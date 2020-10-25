using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace acmun
{
    [Serializable]
    public class Council
    {
        public string Name { get; set; } = "Default Name";
        public int Majority { get; set; } = 0;
        public int SuperMajority { get; set; } = 0;

        public AdminOptions AdminOptions { get; set; } = AdminOptions.Default;

        public Delegation[] Delegations { get; set; } = Delegation.GetDefault();

        static Council LoadCouncilFromFile(string path)
            => HelperMethods.DeserializeXMLFromFile<Council>(path);

        static void WriteConfigToFile(Council council, string path) 
            => HelperMethods.SerializeToXMLFile(path, council);
    }

    [Serializable]
    public class AdminOptions
    {
        public bool GeneratePasswords { get; set; }

        [XmlArrayItem]
        public AdminAccount[] AdminAccounts { get; set; }

        public static AdminOptions Default => new AdminOptions()
        {
            GeneratePasswords = true,
            AdminAccounts = new AdminAccount[] { new AdminAccount("admin", "admin") }
        };
    }
}
