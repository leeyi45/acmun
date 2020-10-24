using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace acmun
{
    [Serializable]
    public class Delegation
    {
        [XmlElement]
        public string FullName { get; set; }
        [XmlElement]
        public string ShortName { get; set; }
        [XmlElement]
        public bool Veto { get; set; }
        [XmlElement]
        public bool Observer { get; set; }

        public static Delegation[] GetDefault()
        {
            var defaultDel = new Delegation()
            {
                FullName = "United States of America",
                ShortName = "USA",
                Veto = false,
                Observer = false
            };
            return new Delegation[] { defaultDel };
        }
    }
}
