using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public struct ObjectInfo
{
    public GameObject       target;
    public Transform        parent;
    public Vector3          initLocalPosition;
    public Quaternion       initLocalRotation;
    public Vector3          initLocalScale;
    public Vector3          initPosition;
    public Quaternion       initRotation;

    public HashSet<GameObject> Parents;
    public ObjectInfo(GameObject target)
    {
        this.target = target;

        var tr = target.transform;
        initLocalPosition = tr.localPosition;
        initLocalRotation = tr.localRotation;
        initLocalScale = tr.localScale;
        initPosition = tr.position;
        initRotation = tr.rotation;
        parent = tr.parent;
        var tmpparent = parent;

        Parents = new HashSet<GameObject>();
        while (tmpparent)
        {
            if (tmpparent != null)
                Parents.Add(tmpparent.gameObject);

            tmpparent = tmpparent.parent;
        }
    }
}

namespace TestPerformance
{
    public class ForeachTests : Test
    {
        public GameObject target;
        public int TargetNumber = 100;

        private List<ObjectInfo> _targets = new List<ObjectInfo>();


        private void Start()
        {
            for (int idx = 0; idx < TargetNumber; ++idx)
                _targets.Add(new ObjectInfo(target));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }


        public override void DoTest()
        {
            base.DoTest();

            // avoiding memory allocate procces impact time
            StartForeachTest(1); StartForTest(1);

            var resultforeach = StartForeachTest(Attempts);

            var resultfor = StartForTest(Attempts);

            WriteResult(TargetNumber.ToString(), resultforeach, resultfor);
        }



        public string StartForeachTest(int attempts)
        {
            timeResult = 0;

            for(int idx=0; idx < attempts; ++idx)
            {
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();

                // Begin timing.
                stopwatch.Start();
                var targetsinfso = _targets.ToArray();
                foreach (var targetinfo in targetsinfso)
                    Release(targetinfo.target);

                // Stop timing.
                stopwatch.Stop();

                timeResult += stopwatch.Elapsed.TotalSeconds;
            }

            timeResult /= attempts;

            // Write result.
            return "Foreach test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }

        public string StartForTest(int attempts)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();

                // Begin timing.
                stopwatch.Start();

                var targetsinfo = _targets.ToArray();
                for (int jdx = 0; jdx < targetsinfo.Length; ++jdx)
                    Release(targetsinfo[jdx].target);

                // Stop timing.
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }

            timeResult /= attempts;

            // Write result.
            return "For test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }

        public void Release(GameObject target)
        {
            //Some actions with target
        }
    }
}
