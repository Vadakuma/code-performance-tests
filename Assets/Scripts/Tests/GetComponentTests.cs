using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace TestPerformance
{
    public class GetComponentTests : Test
    {
        public string TestMethod(int attempts, string name = null)
        {
            double timeResult = 0;
            for (int idx = 0; idx < attempts; ++idx)
            {
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();

                // Begin timing.
                stopwatch.Start();

                // Do something.
                for (int i = 0; i < Counter; i++)
                {
                    var comp = transform.GetComponentInChildren<IData>();
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            // Write result.
            return "Components " + name +" test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }
    }
}