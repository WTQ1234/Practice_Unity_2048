using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelper : SingleTon<DebugHelper>
{
    public bool canDebug = true;
    public void Log(object message)
    {
        if (canDebug)
        {
            Debug.Log(message);
        }
    }
}
