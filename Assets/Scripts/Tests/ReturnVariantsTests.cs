using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TestPerformance
{
    public class ReturnVariantsTests : Test
    {
        public int Counter = 1000;


        public override void DoTest()
        {
            base.DoTest();

            var testOutMethod = TestOutMethod(Attempts);

            var testReturnMethod = TestReturnMethod(Attempts);

            WriteResult(Counter.ToString(), testOutMethod, testReturnMethod);
        }

        private string TestOutMethod(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    OutMethod(out string value);
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Using Out from method test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }


        private string TestReturnMethod(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    var value = ReturnMethod();
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Using Return from method test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }




        private void OutMethod(out string value) { value = "1"; }
        private string ReturnMethod() { return "1"; }
    }
}