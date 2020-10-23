using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using leeyi45.acmun.Controls;
using System.Reflection;
using System.ComponentModel;

namespace leeyi45.acmun
{
    [Serializable]
    [XmlRoot]
    public class Council
    {
        #region Instance Implementation
        private Council() => Present = new Delegation[] { };

        public Council(string name, Delegation[] countries) : this()
        {
            CouncilName = name;
            Delegations = countries;
        }

        public Council(string name, Delegation[] countries, Dictionary<string, MotionData> data, List<MotionData> listData) :
            this(name, countries)
        {
            motions = data;
            motionsAsList = listData;
        }

        public static void LoadDefault()
        {
            Instance = new Council()
            {
                CouncilName = "Null",
                Delegations = { },
                topics = new []{ "1", "2" },
                Settings = SettingsHolder.Default,
                motions = new Dictionary<string, MotionData>()
                {
                    { "unmod", new MotionData("test", "test", "unmod", 2, true, false, false) },
                    { "mod", new MotionData("test", "test", "mod", 1, true, true, true) }
                    //{ }
                }
            };
        }

        [XmlElement(Order = 0)]
        [DefaultValue(null)]
        public string Version
        {
            get => _version;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("No version detected for conf.xml file, using it anyways!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                _version = value;
            }
        }

        private string _version;

        [XmlElement("Name", Order = 1)]
        public string CouncilName { get; set; }

        [XmlArray(Order = 2)]
        [XmlArrayItem("Delegation")]
        public Delegation[] Delegations
        {
            get => _Delegations;
            set
            {
                _Delegations = value;
                DelegationsByName = value.ToDictionary(x => x.Shortf, x => x);
            }
        }

        [XmlIgnore]
        private Delegation[] _Delegations;

        [XmlIgnore]
        public Dictionary<string, Delegation> DelegationsByName { get; set; } = new Dictionary<string, Delegation>();

        [XmlIgnore]
        private Dictionary<string, MotionData> _motions;

        [XmlIgnore]
        public Dictionary<string, MotionData> motions
        {
            get
            {
                try
                {
                    if ((_motions?.Count ?? 0) == 0) _motions = _motionsAsList.ToDictionary(x => x.Id, x => x);
                    return _motions;
                }
                catch (ArgumentException)
                {
                    throw new DataLoadException("Conflicting Motion ID detected");
                }
            }
            set => _motions = motions;
        }

        [XmlElement(Order = 3)]
        public SettingsHolder Settings { get; set; } = new SettingsHolder();

        [XmlArray("Motions", Order = 4)]
        [XmlArrayItem("Motion")]
        public List<MotionData> motionsAsList
        {
            get => _motionsAsList;
            set
            {
                _motionsAsList = value;
                motions = value.ToDictionary(x => x.Id, x => x);
            }
        }

        [XmlIgnore]
        private List<MotionData> _motionsAsList = new List<MotionData>();

        [XmlArray("Topics", Order = 5)]
        [XmlArrayItem("Topic")]
        public string[] topics { get; set; }
        #endregion

        #region Everything else
        private static Council Instance;

        public static string Name
        {
            get => Instance.CouncilName;
            set => Instance.CouncilName = value;
        }

        public static Delegation[] DelList
        {
            get => Instance.Delegations;
            set => Instance.Delegations = value;
        }

        public static Dictionary<string, Delegation> DelsByShortf { get; set; }

        public static int DelCount => DelList.Length;

        public static Delegation[] Present { get; set; }

        public static int PresentCount => Present.Length;

        public static Delegation[] Voters { get; set; }

        public static int VoteCount => Voters.Length;

        public static string[] VotersShortf { get; set; }

        public static string[] PresentShortf { get; set; }

        public static void UpdatePresent()
        {
            Present = DelList.Where(x => x.Present).ToArray();
            Array.Sort(Present);

            Voters = Present.Where(x => !x.Observer).ToArray();
            Array.Sort(Voters);

            DelsByShortf = DelList.ToDictionary(x => x.Shortf, x => x);
            VotersShortf = Voters.Select(x => x.Shortf).ToArray();
            PresentShortf = Present.Select(x => x.Shortf).ToArray();
        }

        #region Settings
        public static bool FiftyPlus1
        {
            get => Instance.Settings.FiftyPlus1;
            set => Instance.Settings.FiftyPlus1 = value;
        }

        public static bool TwoThirdPlus1
        {
            get => Instance.Settings.TwoThirdPlus1;
            set => Instance.Settings.TwoThirdPlus1 = value;
        }

        public static TimeSpan GSLSpeakTime
        {
            get => Instance.Settings.GSLSpeakTime;
            set => Instance.Settings.GSLSpeakTime = value;
        }

        public static TimeSpan UnmodSpeakTime
        {
            get => Instance.Settings.UnmodSpeakTime;
            set => Instance.Settings.UnmodSpeakTime = value;
        }

        public static TimeSpan UnmodSummaryTime
        {
            get => Instance.Settings.UnmodSummaryTime;
            set => Instance.Settings.UnmodSummaryTime = value;
        }

        public static TimeSpan ModSpeakTime
        {
            get => Instance.Settings.ModSpeakTime;
            set => Instance.Settings.ModSpeakTime = value;
        }

        public static TimeSpan ModTotalTime
        {
            get => Instance.Settings.ModTotalTime;
            set => Instance.Settings.ModTotalTime = value;
        }

        public static bool SaveSpeakTime
        {
            get => Instance.Settings.SaveSpeakTime;
            set => Instance.Settings.SaveSpeakTime = value;
        }

        public static bool SaveMotions
        {
            get => Instance.Settings.SaveMotions;
            set => Instance.Settings.SaveMotions = value;
        }

        public static bool TrackDebate
        {
            get => Instance.Settings.TrackDebate;
            set => Instance.Settings.TrackDebate = value;
        }

        public static int DebateSpeakCount
        {
            get => Instance.Settings.DebateSpeakCount;
            set => Instance.Settings.DebateSpeakCount = value;
        }

        public static TimeSpan DebateSpeakTime
        {
            get => Instance.Settings.DebateSpeakTime;
            set => Instance.Settings.DebateSpeakTime = value;
        }

        public static TimeSpan ResoReadTime
        {
            get => Instance.Settings.ResoReadTime;
            set => Instance.Settings.ResoReadTime = value;
        }
        #endregion

        public static Dictionary<string, MotionData> Motions
            => Instance.motions;

        public static List<MotionData> MotionsAsList
        {
            get => Instance.motionsAsList;
            set => Instance.motionsAsList = value;
        }

        public static int Maj50 { get; set; }

        public static int Maj67 { get; set; }

        public static string[] Topics
        {
            get => Instance.topics;
            set => Instance.topics = value;
        }

        public static int CurrentTopic { get; set; } = 0;
        #endregion

        public static bool LoadCouncil(string path, bool exit = false)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Failed to locate file at the specified location while loading council, please try again", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                using (var stream = File.Open(path, FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(Council));
                    Instance = (Council)serializer.Deserialize(stream);
                }

                return true;
            }
            catch (DataLoadException e)
            {
                MessageBox.Show($"There was an error loading the settings xml file: {e.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (exit) Application.Exit();
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show($"There was an error loading the settings xml file: {e.InnerException.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (exit) Application.Exit();
            }
            return false;
        }
    }

    [Serializable]
    public class SettingsHolder
    {
        public bool FiftyPlus1 { get; set; } = false;

        public bool TwoThirdPlus1 { get; set; } = false;

        #region Timespans
        [XmlIgnore]
        public TimeSpan GSLSpeakTime { get; set; } = new TimeSpan(0, 1, 30);

        [XmlIgnore]
        public TimeSpan UnmodSpeakTime { get; set; } = new TimeSpan(0, 5, 0);

        [XmlIgnore]
        public TimeSpan UnmodSummaryTime { get; set; } = new TimeSpan(0, 1, 0);

        [XmlIgnore]
        public TimeSpan ModSpeakTime { get; set; } = new TimeSpan(0, 1, 0);

        [XmlIgnore]
        public TimeSpan ModTotalTime { get; set; } = new TimeSpan(0, 10, 0);

        [XmlIgnore]
        public TimeSpan DebateSpeakTime { get; set; } = new TimeSpan(0, 1, 30);

        [XmlIgnore]
        public TimeSpan ResoReadTime { get; set; } = new TimeSpan(0, 3, 0);
        #endregion

        #region and their double counterparts
        [XmlElement("GSLSpeakTime")]
        public double GSLSpeakTotalSeconds
        {
            get => GSLSpeakTime.TotalSeconds;
            set => GSLSpeakTime = new TimeSpan(0, 0, (int)value);
        }

        [XmlElement("UnmodSpeakTime")]
        public double UnmodSpeakTotalSeconds
        {
            get => UnmodSpeakTime.TotalSeconds;
            set => UnmodSpeakTime = new TimeSpan(0, 0, (int)value);
        }

        [XmlElement("UnmodSummaryTime")]
        public double UnmodSummaryTotalSeconds
        {
            get => UnmodSummaryTime.TotalSeconds;
            set => UnmodSummaryTime = new TimeSpan(0, 0, (int)value);
        }

        [XmlElement("ModSpeakTime")]
        public double ModSpeakTotalSeconds
        {
            get => ModSpeakTime.TotalSeconds;
            set => ModSpeakTime = new TimeSpan(0, 0, (int)value);
        }

        [XmlElement("ModTotalTime")]
        public double ModTotalTotalSeconds
        {
            get => ModTotalTime.TotalSeconds;
            set => ModTotalTime = new TimeSpan(0, 0, (int)value);
        }

        [XmlElement("DebateSpeakTime")]
        public double DebateSpeakTotalSeconds
        {
            get => DebateSpeakTime.TotalSeconds;
            set => DebateSpeakTime = new TimeSpan(0, 0, (int)value);
        }

        [XmlElement("ResoReadTime")]
        public double ResoReadTotalSeconds
        {
            get => ResoReadTime.TotalSeconds;
            set => ResoReadTime = new TimeSpan(0, 0, (int)value);
        }
        #endregion

        public bool SaveSpeakTime { get; set; } = true;

        public bool SaveMotions { get; set; } = true;

        public bool TrackDebate { get; set; } = false;

        public int DebateSpeakCount { get; set; } = 2;

        public static SettingsHolder Default { get; internal set; }

        static SettingsHolder()
        {
            Default = new SettingsHolder();
        }
    }

    [Serializable]
    public class CouncilState
    {
        private CouncilState() { }

        [XmlArrayItem("Speaker")]
        public string[] GSLList { get; set; }

        [XmlArrayItem("Speaker")]
        public string[] ModList { get; set; }

        public ModCaucus CurrentMod { get; set; }

        public UnmodCaucus CurrentUnmod { get; set; }

        public Delegation[] Dels { get; set; }

        [XmlArrayItem("Motion")]
        public static Motion[] Motions { get; set; }

        public static void SaveState(List<string> gslList, List<string> modList, ModCaucus mod, UnmodCaucus unmod)
        {
            try
            {
                using (var stream = File.Open("state.xml", FileMode.OpenOrCreate))
                {
                    var serializer = new XmlSerializer(typeof(CouncilState));
                    serializer.Serialize(stream, new CouncilState
                    {
                        GSLList = gslList.ToArray(),
                        ModList = modList.ToArray(),
                        CurrentMod = mod,
                        CurrentUnmod = unmod,
                        Dels = Council.DelList
                    });

                    stream.Flush();
                    MessageBox.Show("Saved state data to state.xml file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch(Exception e)
            {
                MessageBox.Show($"Unknown error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        public static void LoadState(string path)
        {
            var serializer = new XmlSerializer(typeof(CouncilState));
            try
            {
                using(var stream = File.Open(path, FileMode.Open))
                {
                    var info = (CouncilState)serializer.Deserialize(stream);
                    Program.Instance.LoadState(info);
                    MessageBox.Show("Loaded state data from state.xml file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show($"Failed to locate the state file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unknown error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }
    }
   
}
