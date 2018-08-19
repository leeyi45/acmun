using System;
using System.Xml.Serialization;

namespace leeyi45.acmun
{
    [Serializable]
    public class Delegation : IComparable<Delegation>
    {
        private Delegation() { }

        public Delegation(string name, string shortf, bool present = false)
        {
            Name = name;
            Shortf = shortf;
            Present = present;
        }

        [XmlElement]
        public string Name
        {
            get => _name;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new MissingDataException("Country name cannot be empty or whitespace!");
                }
                else _name = value;
            }
        }

        private string _name = null;

        [XmlElement]
        public string AltName
        {
            get => string.IsNullOrWhiteSpace(altName) ? Shortf : altName;
            set => altName = value;
        }

        private string altName = String.Empty;

        [XmlElement]
        public string Shortf
        {
            get => string.IsNullOrWhiteSpace(_shortf) ? Name : _shortf;
            set => _shortf = value;
        }

        private string _shortf = String.Empty;

        [XmlElement]
        public bool Observer { get; set; } = false;

        [XmlElement]
        public bool P5Veto { get; set; } = false;

        [XmlIgnore]
        public bool Present { get; set; }

        [XmlIgnore]
        public TimeSpan SpeakingTime { get; set; }

        [XmlElement]
        public int SpeechCount { get; set; } = 0;

        [XmlElement("Speaking Time")]
        public string SpeakingTimeStr
        {
            get => SpeakingTime.ToString(@"hh\:mm\:ss");
            set => SpeakingTime = TimeSpan.Parse(value);
        }

        public bool ShouldSerializeSpeakingTimeStr()
            => SpeakingTime != TimeSpan.Zero;

        public int CompareTo(Delegation other)
            => Shortf.CompareTo(other.Shortf);
    }
}
