using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TestPerformance
{
    public class Test : MonoBehaviour
    {
        public Text debugField;
        public int Attempts = 1;


        protected double timeResult;

        public void WriteResult(string numberoftergets, string result1, string result2 )
        {
            var totalresult = numberoftergets + "\n";
            totalresult += result1 + "\n";
            totalresult += result2 + "\n";
            if (debugField)
            {
                debugField.text = totalresult;
            }

            Debug.Log(totalresult);
        }

        public virtual void DoTest()
        {

        }
    }


}

