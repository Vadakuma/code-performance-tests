using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace TestPerformance
{
    public class LinqTests : Test
    {
        private List<Item> _items = new List<Item>();

        public int ItemAmount = 10000;
        public int searchIndex = 499;
        // Start is called before the first frame update
        void Start()
        {
            for (int idx = 0; idx < ItemAmount; ++idx)
                _items.Add(new ItemFloat(idx, idx));
        }


        public override void DoTest()
        {
            base.DoTest();
            // avoiding memory allocate procces impact time
            //GetItemFromFor(1, _items);

            var resultlinq = StartLinqTest(Attempts, searchIndex, _items);
            var resultfor = StartForTest(Attempts, searchIndex, _items);

            WriteResult(ItemAmount.ToString(), resultlinq, resultfor);
        }

        public string StartForTest(int attempts, float time, List<Item> keys)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                GetItemFromFor(time, keys);
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "For test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }


        public string StartLinqTest(int attempts, float time, List<Item> keys)
        {
            timeResult = 0;

            for (int idx = 0; idx < attempts; ++idx)
            {
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                GetKeyFromLinqTest(time, keys);
                stopwatch.Stop();
                timeResult += stopwatch.Elapsed.TotalSeconds;
            }
            timeResult /= attempts;

            return "Linq test.  Time elapsed:  " + timeResult.ToString() + " in seconds";
        }


        public static Item GetItemFromFor(float time, List<Item> keys)
        {
            var keysCount = keys.Count;
            if (keysCount == 0)
                return null;

            // If before the first key.
            if (time <= keys[0].time)
            {
                return keys[0];
            }
            // After the last key.
            else if (time >= keys[keysCount - 1].time)
            {
                return keys[keysCount - 1];
            }
            // Somewhere in between.
            else
            {
                // An index of a current key.
                int index = -1;// keys.FindLastIndex(p => p.time <= time);
                               // optimization
                for (int idx = 0; idx < keysCount; ++idx)
                {
                    var key = keys[idx];
                    if (key.time <= time)
                        index = idx;
                    else if (key.time > time)
                        break;
                }

                // If the index not found - return the first key in the list.
                if (index == -1)
                {
                    return keys[0];
                }
                // Return the last key in case if the index is the last.
                else if (index == keysCount - 1)
                {
                    return keys[keysCount - 1];
                }
                // When the index is found and it is not the last one --
                // get an interpolated key
                else
                {
                    Item currentKey = keys[index];
                    Item nextKey = keys[index + 1];
                    return currentKey.InterpolateTo(nextKey, time);
                }
            }
        }

        public static Item GetKeyFromLinqTest(float time, List<Item> keys)
        {
            if (keys.Count == 0)
                return null;

            // If before the first key.
            if (time <= keys[0].time)
            {
                return keys[0];
            }
            // After the last key.
            else if (time >= keys[keys.Count - 1].time)
            {
                return keys[keys.Count - 1];
            }
            // Somewhere in between.
            else
            {
                // An index of a current key.
                int index = keys.FindLastIndex(p => p.time <= time);
                // If the index not found - return the first key in the list.
                if (index == -1)
                {
                    return keys[0];
                }
                // Return the last key in case if the index is the last.
                else if (index == keys.Count - 1)
                {
                    return keys[keys.Count - 1];
                }
                // When the index is found and it is not the last one --
                // get an interpolated key
                else
                {
                    Item currentKey = keys[index];
                    Item nextKey = keys[index + 1];
                    return currentKey.InterpolateTo(nextKey, time);
                }
            }
        }
    }
}


public class ItemFloat : Item
{
    private static ItemFloat cashedKeyFloat;
    private float _inTangent;
    private float _outTangent;
    private float _inWeight = 0.333f;
    private float _outWeight = 0.333f;

    public float value;

    public float inTangent
    {
        get
        {
            return _inTangent;
        }
        set
        {
            var val = value;

            if (float.IsNaN(val))
            {
               // Debug.LogError("Tried to set NaN for InTangent! It's not allowed. InTangent is set to 0");
                val = 0;
            }

            _inTangent = Mathf.Clamp(val, float.MinValue, float.MaxValue);
        }
    }
    public float outTangent
    {
        get
        {
            return _outTangent;
        }
        set
        {
            var val = value;

            if (float.IsNaN(val))
            {
               // Debug.LogError("Tried to set NaN for OutTangent! It's not allowed. OutTangent is set to 0.");
                val = 0;
            }

            _outTangent = Mathf.Clamp(val, float.MinValue, float.MaxValue);
        }
    }

    public float inWeight
    {
        get
        {
            return _inWeight;
        }
        set
        {
            var val = value;

            if (float.IsNaN(val))
            {
               // Debug.LogError("Tried to set NaN for InWeight! It's not allowed. InWeight is set to 0.");
                val = 0;
            }

            _inWeight = Mathf.Clamp(val, float.MinValue, float.MaxValue);
        }
    }

    public float outWeight
    {
        get
        {
            return _outWeight;
        }
        set
        {
            var val = value;

            if (float.IsNaN(val))
            {
              //  Debug.LogError("Tried to set NaN for OutWeight! It's not allowed. OutWeight is set to 0.");
                val = 0;
            }

            _outWeight = Mathf.Clamp(val, float.MinValue, float.MaxValue);
        }
    }

    public ItemFloat(float value, float time)
    {
        this.value = value;
        base.time = time;
       // this.interpolation = interpolation;
    }

    public override bool Equals(Item key)
    {
        return value == (key as ItemFloat).value;
    }

    public override string GetValueType()
    {
        return "float";
    }

    public override string GetStringFromValue()
    {
        return value.ToString();
    }

    public static float GetValueFromString(string valueString)
    {
        return float.Parse(valueString);
    }

    public override Item InterpolateLinearlyTo(Item key, float t)
    {
        if (cashedKeyFloat == null)
        {
            cashedKeyFloat = new ItemFloat(value, t);
        }

        cashedKeyFloat.time = t;
        var portion = Mathf.InverseLerp(time, key.time, t);
        cashedKeyFloat.value = Mathf.Lerp(value, (key as ItemFloat).value, portion);

        return cashedKeyFloat;
    }

    public static Vector2 GetInHandle(ItemFloat prevKey, ItemFloat key)
    {
        var adjacentIn = key.inWeight * (key.time - prevKey.time);
        var oppositeIn = key.inTangent * adjacentIn;

        var handleIn = new Vector2(
            key.time - adjacentIn,
            key.value - oppositeIn
        );

        return handleIn;
    }

    public static Vector2 GetOutHandle(ItemFloat key, ItemFloat nextKey)
    {
        var adjacentOut = key.outWeight * (nextKey.time - key.time);
        var oppositeOut = key.outTangent * adjacentOut;

        var handleOut = new Vector2(
            key.time + adjacentOut,
            key.value + oppositeOut
        );

        return handleOut;
    }

    public override Item InterpolateByBezierTo(Item key, float t)
    {
        var targetKey = key as ItemFloat;

        if (cashedKeyFloat == null)
        {
            cashedKeyFloat = new ItemFloat(value, t);
        }

        var handleA = GetOutHandle(this, targetKey);
        var handleB = GetInHandle(this, targetKey);
        var portion = Mathf.InverseLerp(time, key.time, t);

        cashedKeyFloat.value = BezierCubic(portion, value, handleA.y, handleB.y, targetKey.value);

        return cashedKeyFloat;
    }

    protected override Item GetCloned()
    {
        var key = new ItemFloat(value, time)
        {
            inTangent = inTangent,
            outTangent = outTangent,
            inWeight = inWeight,
            outWeight = outWeight
        };

        return key;
    }
}
public abstract class Item
{
    public const float DEFAULT_HANDLE_SCALAR = 0.333f;

    public bool manualInterpolation;

    public float time;

    private float _handleScalar = DEFAULT_HANDLE_SCALAR;

    public float handleScalar
    {
        get
        {
            return _handleScalar;
        }
        set
        {
            if (float.IsNaN(value))
            {
                _handleScalar = DEFAULT_HANDLE_SCALAR;
               // Debug.LogError("Handle Scalar was set to NaN!");
                return;
            }

            _handleScalar = value;
        }
    }

    public static float BezierCubic(float t, float pointA, float handleA, float handleB, float pointB)
    {
        var t2 = t * t;
        var t3 = t2 * t;
        var mt = 1f - t;
        var mt2 = mt * mt;
        var mt3 = mt2 * mt;
        return pointA * mt3 + 3f * handleA * mt2 * t + 3f * handleB * mt * t2 + pointB * t3;
    }

    /// <summary>
    /// Compare all but time.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public virtual bool Equals(Item key)
    {
        return true;
    }

    protected virtual Item GetCloned()
    {
        return null;
    }

    /// <summary>
    /// Return a duplicate of the key.
    /// </summary>
    /// <returns></returns>
    public Item Clone()
    {
        var key = GetCloned();
        key.manualInterpolation = manualInterpolation;

        return key;
    }

    public virtual string GetValueType()
    {
        return "unknown";
    }

    public virtual string GetStringFromValue()
    {
        return null;
    }

    public virtual string GetStringFromLeftHandle()
    {
        return string.Empty;
    }

    public virtual string GetStringFromRightHandle()
    {
        return string.Empty;
    }

    public virtual Item InterpolateLinearlyTo(Item key, float t)
    {
        return this;
    }

    public virtual Item InterpolateByBezierTo(Item key, float t)
    {
        return this;
    }
    public virtual Item InterpolateTo(Item key, float t)
    {
        return InterpolateLinearlyTo(key, t);
    }
}