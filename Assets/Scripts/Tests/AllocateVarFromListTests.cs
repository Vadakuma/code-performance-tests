using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace TestPerformance
{
    public class AllocateVarFromListTests : Test
    {
        public int Counter = 1000;


        private List<string> _testlist = new List<string>();

        void Start()
        {
            for (int idx = 0; idx < 1000; ++idx)
                _testlist.Add(System.Guid.NewGuid().ToString());
        }

        public override void DoTest()
        {
            base.DoTest();

            var testOneAllocating = TestOneAllocating(Attempts);

            var testTenAllocating = TestTenAllocating(Attempts);

            WriteResult(Counter.ToString(), testOneAllocating, testTenAllocating);
        }

        private string TestOneAllocating(int attempts)
        {
            timeResult = 0;
            var index = 3;
            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    var value = _testlist[index];
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "One allocating per for.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }


        private string TestTenAllocating(int attempts)
        {
            timeResult = 0;
            var index = 3;
            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    var value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                    value = _testlist[index];
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "10 allocating per for.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }
    }
}