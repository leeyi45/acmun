using System;
using System.Xml.Serialization;

namespace leeyi45.acmun
{
    [Serializable]
    public class Country : IComparable<Country>
    {
        private Country() { }

        public Country(string name, string shortf, bool present = false)
        {
            Name = name;
            Shortf = shortf;
            Present = present;
        }

        [XmlElement]
        public string Name { get; set; }

        private string altName = String.Empty;

        [XmlElement]
        public string AltName
        {
            get => string.IsNullOrWhiteSpace(altName) ? Shortf : altName;
            set => altName = value;
        }

        [XmlElement]
        public string Shortf { get; set; }

        [XmlElement]
        public bool Observer { get; set; }

        [XmlElement]
        public bool P5Veto { get; set; }

        [XmlIgnore]
        public bool Present { get; set; }

        [XmlIgnore]
        public TimeSpan SpeakingTime { get; set; }

        [XmlElement("Speaking Time")]
        public string SpeakingTimeStr
        {
            get => SpeakingTime.ToString(@"hh\:mm\:ss");
            set => SpeakingTime = TimeSpan.Parse(value);
        }

        public bool ShouldSerializeSpeakingTimeStr() 
            => SpeakingTime != TimeSpan.Zero;

        public int CompareTo(Country other)
            => Shortf.CompareTo(other.Shortf);
    }
}
