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

            StateVals = new Dictionary<VoteState, string>
            {
                { VoteState.Pass, "Pass" },
                { VoteState.Fail, "Fail" },
                { VoteState.PassD, "Passed by discretion" },
                { VoteState.FailD, "Failed by discretion" },
                { VoteState.Null, "" }
            };

            if (!Council.LoadCouncil("conf.xml", true))
            {
                Council.LoadDefault();
            }
            Instance = new Main_Screen.homescreen();
            Application.Run(Instance);
        }

        public static string ToValString(this TimeSpan span)
            => span.ToString(@"mm\:ss");

        public static string ToValString(this VoteState state)
            => StateVals[state];

        private static Dictionary<VoteState, string> StateVals;
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
