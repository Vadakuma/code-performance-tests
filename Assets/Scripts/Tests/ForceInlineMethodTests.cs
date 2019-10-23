using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TestPerformance
{
    public class ForceInlineMethodTests : Test
    {
        public int Counter = 1000;


        public override void DoTest()
        {
            base.DoTest();

            var testInlineMethod = TestInlineMethod(Attempts);

            var testNotInlineMethod = TestNotInlineMethod(Attempts);

            WriteResult(Counter.ToString(), testInlineMethod, testNotInlineMethod);
        }


        private string TestInlineMethod(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    InlineMethod();
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Inline method test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }


        private string TestNotInlineMethod(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    NotInlineMethod();
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Not inline method test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InlineMethod() { }
        private void NotInlineMethod() { }
    }
}