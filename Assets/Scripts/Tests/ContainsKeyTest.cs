using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TestPerformance
{
    public class ContainsKeyTest : Test
    {
        public int Counter = 1000;
        public int DictionaryCounter = 3000;

        private Dictionary<string, string>  _dict = new Dictionary<string, string>();
        private string                      _keyrandom;

        // Start is called before the first frame update
        void Start()
        {
            for (int idx = 0; idx < DictionaryCounter; ++idx)
                _dict.Add(System.Guid.NewGuid().ToString(), System.Guid.NewGuid().ToString());
        }

        public override void DoTest()
        {
            base.DoTest();
            //_keyrandom = Random.Range(_dict.Count/2, _dict.Count - 1);
            _keyrandom = System.Guid.NewGuid().ToString();
            // avoiding memory allocate procces impact time
            TestContainsKey(1);

            var resultContainsKey = TestContainsKey(Attempts);

            var resulttrygetvalue = TestTryGetValue(Attempts);

            WriteResult(Counter.ToString(), resultContainsKey, resulttrygetvalue);
        }


        private string TestContainsKey(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    if (_dict.ContainsKey(_keyrandom))
                    {
                        var value = _dict[_keyrandom];
                    }
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Dictionary ContainsKey test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }

        private string TestTryGetValue(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

                for (int jdx = 0; jdx < Counter; ++jdx)
                {
                    if(_dict.TryGetValue(_keyrandom, out string value))
                    {
                        //do something with value
                    }
                }

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Dictionary TryGetValue test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }
    }
}