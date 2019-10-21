using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TestPerformance
{
    public class StringTests : Test
    {
        public int Counter = 1000;


        public override void DoTest()
        {
            base.DoTest();
            // avoiding memory allocate procces impact time
            DoStringWithBoxing(1);

            var resultboxing = DoStringWithBoxing(Attempts);

            var resultwithout = DoStringWithOutBoxing(Attempts);

            WriteResult(Counter.ToString(), resultboxing, resultwithout);
        }

        private string DoStringWithBoxing(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    var str = jdx + "|" + jdx + "|" + jdx;
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "String boxing test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }

        private string DoStringWithOutBoxing(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    var str = jdx.ToString() + "|" + jdx.ToString() + "|" + jdx.ToString();
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "String with int.ToString() test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }
    }
}