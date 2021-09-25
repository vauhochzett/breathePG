using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timings
{
    public static IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
}
