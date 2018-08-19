using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace leeyi45.acmun
{
    static class Program
    {
        public static Main_Screen.homescreen Instance;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            StateVals = new Dictionary<State, string>
            {
                { State.Pass, "Pass" },
                { State.Fail, "Fail" },
                { State.PassD, "Passed by discretion" },
                { State.FailD, "Failed by discretion" },
                { State.Null, "" }
            };

            Council.LoadCouncil("conf.xml", true);
            Instance = new Main_Screen.homescreen();
            Application.Run(Instance);
        }

        public static string ToValString(this TimeSpan span)
            => span.ToString(@"mm\:ss");

        public static string ToValString(this State state)
            => StateVals[state];

        private static Dictionary<State, string> StateVals;
    }

    public class MissingDataException : Exception
    {
        public MissingDataException(string Message) : base(Message) { }
    }

    public class DataLoadException : Exception
    {
        public DataLoadException(string msg) : base(msg) { }
    }
}
