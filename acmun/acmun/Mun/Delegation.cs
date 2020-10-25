using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace acmun
{
    [Serializable]
    public class Delegation
    {
        public Delegation(string fullName, string shortName, bool veto, bool observer)
        {
            FullName = fullName;
            ShortName = shortName;
            Veto = veto;
            Observer = observer;
        }

        Delegation() : this("United States of America", "USA", true, false) { }

        public string FullName { get; set; }
        
        public string ShortName { get; set; }
        
        public bool Veto { get; set; }
        
        public bool Observer { get; set; }

        public static Delegation[] GetDefault() => new Delegation[] { new Delegation() };
    }
}
