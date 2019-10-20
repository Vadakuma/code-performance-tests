using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TestPerformance
{
    public class FieldVsProperty : Test
    {
        public int Counter = 1000;

        public GameObject target;

        public GameObject Target
        {
            get
            {
                if (target == null)
                    return null;
                return target;
            }
        }


        public override void DoTest()
        {
            base.DoTest();
            // avoiding memory allocate procces impact time
            GetField(1); GetProperty(1);

            var resultfield = GetField(Attempts);

            var resultprop = GetProperty(Attempts);

            WriteResult(Counter.ToString(), resultfield, resultprop);
        }


        public string GetField(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    var t = target;
                    if (t)
                        continue;
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "GetField test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }

        public string GetProperty(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    var t = Target;
                    if (t)
                        continue;
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }

            timeResult /= attempts;

            return "GetProperty test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }
    }
}
