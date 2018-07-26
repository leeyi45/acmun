using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace leeyi45.acmun
{
    static class Program
    {
        public static Main_Screen.homescreen Instance;

        [STAThread]
        static void Main(string[] args)
        {
            //using (var stream = System.IO.File.Open("conf.xml", System.IO.FileMode.Open))
            //{
            //    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Council));
            //    var instance = (Council)serializer.Deserialize(stream);
            //    instance.topics = new[] { "generic 1", "generic 2", "generic 3" };
            //    Country.Serial = false;
            //    serializer.Serialize(stream, instance);
            //}

            StateVals = new Dictionary<State, string>
            {
                { State.Pass, "Pass" },
                { State.Fail, "Fail" },
                { State.PassD, "Passed by discretion" },
                { State.FailD, "Failed by discretion" },
                { State.Null, "" }
            };

            Council.LoadCouncil("conf.xml");
            Instance = new Main_Screen.homescreen();

            Application.EnableVisualStyles();
            Application.Run(Instance);
        }

        public static string ToValString(this TimeSpan span)
            => span.ToString(@"mm\:ss");

        public static string ToValString(this State state)
            => StateVals[state];

        private static Dictionary<State, string> StateVals;
    }
}
