using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace acmun
{
    [Serializable]
    public class Account
    {
        public Account(string Username, string Password, bool IsAdmin)
        {
            this.Username = Username;
            this.Password = Password;
            this.IsAdmin = IsAdmin;
        }

        public string Username { get; }
        public string Password { get; }
        public bool IsAdmin { get; }

        [XmlIgnore]
        public string SessId { get; set; }

        public static List<Account> CurrentUsers { get; }
    }

    [Serializable]
    public class AdminAccount : Account
    {
        public AdminAccount(string Username, string Password) : base(Username, Password, true) { }
    }
}
