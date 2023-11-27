using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class State_Base<T> where T : System.Enum
{
    public T name;
    // 当前状态能够指向的其他状态 此处暂时存的是名字
    private List<T> transList = new List<T>();

    public State_Base(StateData<T> stateData)
    {
        name = stateData.name;
        transList = stateData.transitions.ToList();
    }

    #region Trans
    public void SetTransitions(List<T> names)
    {
        transList = names;
    }

    public void AddTransition(T name)
    {
        if (!transList.Contains(name))
        {
            transList.Add(name);
        }
    }

    public bool FindTransition(T name)
    {
        return transList.Contains(name);
    }
    #endregion

    #region Logic
    public void Enter()
    {
        DebugHelper.Instance.Log($"State Enter: {name.ToString()}");
    }

    public void Exit()
    {
        DebugHelper.Instance.Log($"State Exit: {name.ToString()}");
    }
    #endregion
}
