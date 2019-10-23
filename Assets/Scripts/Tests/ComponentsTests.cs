using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestPerformance
{
    public class ComponentsTests : Test
    {
        public int TargetNumber = 100;
        public GetComponentTests notoptimized;
        public GetComponentTests optimized;

        // Start is called before the first frame update
        void Start()
        {
            notoptimized.gameObject.SetActive(false);
            optimized.gameObject.SetActive(false);
        }

        public override void DoTest()
        {
            base.DoTest();
            // avoiding memory allocate procces impact time
            notoptimized.TestMethod(Attempts, "not optimized");

            notoptimized.gameObject.SetActive(true);
            var resultnotoptimized = notoptimized.TestMethod(Attempts, "not optimized");
            notoptimized.gameObject.SetActive(false);

            optimized.gameObject.SetActive(true);
            var resultoptimized = optimized.TestMethod(Attempts, "simplified");
            optimized.gameObject.SetActive(false);


            WriteResult(TargetNumber.ToString(), resultnotoptimized, resultoptimized);
        }
    }
}
