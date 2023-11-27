using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction
{
    protected ActionData actionData;
    protected Entity owner;
    protected bool inited = false;
    protected bool executed = false;
    public void Init(ActionData actionData, Entity owner)
    {
        if (inited) return;
        inited = true;
        this.owner = owner;
        this.actionData = actionData;
    }

    private float curTime = 0f;
    public bool Update()
    {
        if (executed) return true;
        if (actionData.offsetTime > 0)
        {
            curTime += Time.deltaTime;
            if (curTime > actionData.offsetTime)
            {
                Execute();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Execute();
            return true;
        }
    }

    protected virtual void Execute()
    {
        executed = true;
        DebugHelper.Instance.Log($"Execute! {this.GetType()}");
    }
}
