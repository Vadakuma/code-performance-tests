using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace TestPerformance
{
    public class RemoveListVSDictionary : Test
    {
        private List<string>                _testList = new List<string>();
        private Dictionary<string, string>  _testDict = new Dictionary<string, string>();


        void Start()
        {
            for (int idx = 0; idx < Counter; ++idx)
            {
                _testList.Add(System.Guid.NewGuid().ToString());
                _testDict.Add(System.Guid.NewGuid().ToString(), System.Guid.NewGuid().ToString());
            }
        }


        public override void DoTest()
        {
            base.DoTest();
            // avoiding memory allocate procces impact time
            DoRemoveFromList(1);

            var resultlist = DoRemoveFromList(Attempts);

            var resultdict = DoRemoveFromDictionary(Attempts);

            WriteResult(Counter.ToString(), resultlist, resultdict);
        }


        private string DoRemoveFromList(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                var tmplist = new List<string>(_testList);

                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    tmplist.Remove(tmplist[0]);
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Remove from list test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }


        private string DoRemoveFromDictionary(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                var tmpdict = new Dictionary<string, string>(_testDict);
                var keyslist = tmpdict.Keys.ToList();

                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    tmpdict.Remove(keyslist[jdx]);
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Remove from dictionary test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }
    }
}