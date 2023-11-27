using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMComponent<T> : MonoBehaviour where T : System.Enum
{
    public State_Base<T> curState;
    private Dictionary<T, State_Base<T>> dict = new Dictionary<T, State_Base<T>>();

    public bool SetFSM(FSMData<T> fsmData)
    {
        foreach(StateData<T> stateData in fsmData.stateDatas)
        {
            State_Base<T> state = new State_Base<T>(stateData);
            dict.Add(stateData.name, state);
        }
        if (dict.TryGetValue(fsmData.curState, out State_Base<T> curState))
        {
            this.curState = curState;
            curState.Enter();
            return true;
        }
        return false;
    }

    public T GetFSMType()
    {
        return curState.name;
    }

    public State_Base<T> GetFSM()
    {
        return curState;
    }

    private void EnterState()
    {

    }

    public bool ForceState(T name)
    {
        if (curState == null) return false;
        // 无视通路，直接切换状态
        if (dict.TryGetValue(name, out State_Base<T> nextState))
        {
            curState?.Exit();
            curState = nextState;
            curState.Enter();
            return true;
        }
        return false;
    }

    public bool ChangeState(T name)
    {
        if (curState == null) return false;
        // 先根据 name 在当前的状态中寻找通路
        if (curState.FindTransition(name))
        {
            // 再根据通路切换状态
            if (dict.TryGetValue(name, out State_Base<T> nextState))
            {
                curState?.Exit();
                curState = nextState;
                curState.Enter();
                return true;
            }
        }
        return false;
    }
}
public class FSMData<T> where T : System.Enum
{
    // 用String来表示当前State的名字
    public T curState;
    public List<StateData<T>> stateDatas;
    public FSMData(T curState, (T, T[], string)[] datas)
    {
        this.curState = curState;
        this.stateDatas = new List<StateData<T>>();
        for (int i = 0; i < datas.Length; i++)
        {
            var data = datas[i];
            AddStateData(data.Item1, data.Item2, data.Item3);
        }
    }

    public void AddStateData(T name, T[] transitions, string type)
    {
        StateData<T> stateData = new StateData<T>(name, transitions, type);
        stateDatas.Add(stateData);
    }
}
// todo 如果用泛型T来做枚举的约束，是否需要对每个枚举做一个子类？
public class StateData<T> where T : System.Enum
{
    public T name;
    public string type;                 // 后续如果需要不同子类状态，使用此type属性寻找子类的名字
    public T[] transitions;    // 可通向的其他状态

    public StateData(T name, T[] transitions, string type = default)
    {
        this.name = name;
        this.transitions = transitions;

        this.type = type;
    }
}
