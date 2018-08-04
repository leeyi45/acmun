using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using static leeyi45.acmun.Council;

namespace leeyi45.acmun
{
    [Serializable]
    public class MotionData : IComparable<MotionData>
    {
        public MotionData(string text, string name, string id, int disruptiveness, bool duration = false,
            bool speakTime = false, bool topic = false)
        {
            Text = text;
            Name = name;
            Id = id;
            Disruptiveness = disruptiveness;
            Duration = duration;
            SpeakTime = speakTime;
            Topic = topic;
        }

        private MotionData() { }

        [XmlElement]
        public int Disruptiveness { get; set; }

        [XmlElement]
        public string Text
        {
            get => _text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DataLoadException("Text value for motion data cannot be null or empty");
                else _text = value;
            }
        }

        private string _text = null;

        [XmlElement]
        public string Id
        {
            get => _id;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new MissingDataException("Motion ID cannot be null");
                else _id = value;
            }
        }

        private string _id = null;

        [XmlElement]
        public string Name
        {
            get => string.IsNullOrWhiteSpace(_name) ? Text : Name;
            set => _name = value;
        }

        private string _name = null;

        [XmlElement]
        public bool Duration { get; set; }

        [XmlElement]
        public bool SpeakTime { get; set; }

        [XmlElement]
        public bool Topic { get; set; }

        [XmlElement]
        public Vote VoteData { get; set; }

        //public static List<MotionData> Motions { get; set; }

        //public static void UpdateMotions()
        //{
        //    try
        //    {
        //        Motions = new List<MotionData>();
        //        var doc = XDocument.Load(@"motions.xml");

        //        foreach (var each in doc.Root.Elements("Motion"))
        //        {
        //            var id = each.Element("Id").Value;
        //            Motions.Add(new MotionData
        //            {
        //                Disruptiveness = int.Parse(each.Element("Disrupt").Value),
        //                Text = each.Element("Text").Value,
        //                Name = each.Element("Name").Value,
        //                Id = id,
        //                Duration = each.Element("Duration").Value.Equals("true", StringComparison.OrdinalIgnoreCase),
        //                SpeakTime = each.Element("SpeakTime").Value.Equals("true", StringComparison.OrdinalIgnoreCase),
        //                Topic = each.Element("Topic").Value.Equals("true", StringComparison.OrdinalIgnoreCase)
        //            });
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Failed to load motions.xml!\nPlease check your configuration",
        //            "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //static MotionData() => UpdateMotions();

        public int CompareTo(MotionData other)
        => Disruptiveness - other.Disruptiveness;
    }

    [Serializable]
    public class Vote
    {
        public int ToPass
        {
            get
            {
                switch (Type)
                {
                    case VoteType.Substantive:
                        {
                            return Council.Maj67;
                        }
                    case VoteType.Procedural:
                        {
                            return Council.Maj50;
                        }
                    default:
                        {
                            return Council.VoteCount;
                        }
                }
            }
        }

        public VoteType Type { get; set; } = VoteType.Procedural;

        public string Topic { get; set; } = "Generic Vote";

        public bool P5Veto { get; set; } = false;

        public bool AllowAbstentions { get; set; } = false;

        public bool HouseDivided { get; set; } = false;

        public bool Passed { get; set; } = false;

        public bool ObserversVote { get; set; } = true;

        public enum VoteType
        {
            Procedural = 0, Substantive = 1, Consensus = 2
        }

        public static Vote Default { get; private set; } = new Vote();
    }

    public enum State
    {
        Pass, Fail, PassD, FailD, Null
    }

    [Serializable]
    public class Motion : IComparable<Motion>
    {
        public Motion(string data, string topic, Country Proposer, TimeSpan Duration, 
            TimeSpan SpeakTime) : this(data, topic, Proposer)
        {
            var errMsg = "Missing Data: ";

            if (Internal.Duration && Duration == TimeSpan.Zero) errMsg += "Total duration\n";
            else this.Duration = Duration;

            if (Internal.SpeakTime && Duration == TimeSpan.Zero) errMsg += "Speaking Time\n";
            else this.SpeakTime = SpeakTime;


            if (errMsg != "Missing Data: ") throw new MissingDataException(errMsg);
        }
        
        public Motion(string _data, string topic, Country Proposer)
        {
            if(!Motions.TryGetValue(_data, out var data))
            {
                throw new MissingDataException($"Failed to load motion with id '{_data}'");
            }

            Internal = data;
            Topic = topic;
            this.Proposer = Proposer;
            var errMsg = "Missing Data: ";

            if (Internal.Topic && string.IsNullOrWhiteSpace(topic)) errMsg += "Topic\n";

            if (errMsg != "Missing Data: ") throw new MissingDataException(errMsg);
        }

        protected Motion() { }

        [XmlIgnore]
        public MotionData Internal { get; set; }

        /*
        [XmlElement("ID")]
        public string InternalID
        {
            get => Internal.Id;
            set => Internal = Motions[value];
        }*/

        [XmlIgnore]
        public string Text
        {
            get => Internal.Text;
            set => Internal.Text = value;
        }

        [XmlElement]
        public string Name
        {
            get => Internal.Name;
            set => Internal.Name = value;
        }

        [XmlElement("ID")]
        public string TypeId
        {
            get => Internal.Id;
            set
            {
                if (!Motions.TryGetValue(value, out var it))
                {
                    throw new DataLoadException($"Could not find motion type with id '{value}'");
                }
                else Internal = it;
            }
        }

        [XmlElement]
        public int Disruptiveness
        {
            get => Internal.Disruptiveness;
            set => Internal.Disruptiveness = value;
        }

        [XmlElement("Duration")]
        public double DurationSeconds
        {
            get => Duration.TotalSeconds;
            set => Duration = new TimeSpan(0, (int)value, 0);
        }

        [XmlIgnore]
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;

        [XmlIgnore]
        public bool HasDuration
        {
            get => Internal.Duration;
            set => Internal.Duration = value;
        }

        [XmlElement("SpeakTime")]
        public double SpeakTimeSeconds
        {
            get => SpeakTime.TotalSeconds;
            set => SpeakTime = new TimeSpan(0, (int)value, 0);
        }

        [XmlIgnore]
        public TimeSpan SpeakTime { get; set; } = TimeSpan.Zero;

        [XmlIgnore]
        public bool HasSpeakTime
        {
            get => Internal.SpeakTime;
            set => Internal.SpeakTime = value;
        }

        [XmlElement]
        public Country Proposer { get; set; }

        [XmlElement]
        public string Topic { get; set; }

        [XmlIgnore]
        public bool HasTopic
        {
            get => Internal.Topic;
            set => Internal.Topic = value;
        }

        [XmlElement]
        public State State { get; set; } = State.Null;

        public int CompareTo(Motion other)
        {
            if (Disruptiveness > other.Disruptiveness) return -1;
            else if (Disruptiveness < other.Disruptiveness) return 1;
            else
            {
                if (other.Duration == Duration) return 0;
                else
                {
                    if (other.Duration == TimeSpan.Zero || Duration == TimeSpan.Zero) return 0;
                    else return (int)(other.Duration - Duration).TotalSeconds;
                }
            }
        }
    }

    [Serializable]
    public class ModCaucus : Motion
    {
        public ModCaucus(string Topic, Country Proposer, TimeSpan Duration, TimeSpan SpeakTime) :
            base("mod", Topic, Proposer, Duration, SpeakTime) 
            => SpeakerCount = (int)(Duration.TotalSeconds / SpeakTime.TotalSeconds);

        private ModCaucus() { }

        [XmlIgnore]
        public int SpeakerCount { get; private set; }

        public static ModCaucus DefaultMod => new ModCaucus("Topic", null, ModTotalTime, ModSpeakTime);
    }

    [Serializable]
    public class UnmodCaucus : Motion
    {
        public UnmodCaucus(string Topic, Country Proposer, TimeSpan Duration) :
            base("unmod", Topic, Proposer, Duration, TimeSpan.Zero)
            => this.Topic = Topic;

        private UnmodCaucus() { }

        public static UnmodCaucus DefaultUnmod => new UnmodCaucus("Topic", null, UnmodSpeakTime);
    }

}