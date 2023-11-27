using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillData skillData;
    public List<BaseAction> actions = new List<BaseAction>();
    public bool isEnd = false;

    private Entity owner;

    public Skill(SkillData skillData, Entity owner)
    {
        this.owner = owner;
        this.skillData = skillData;
        for (int i = 0; i < skillData.actions.Count; i++)
        {
            ActionData actionData = skillData.actions[i];
            Type actionType = TypeHelper.GetSubClassType(typeof(BaseAction), actionData.scriptInfo.name);
            BaseAction action = Activator.CreateInstance(actionType) as BaseAction;
            action.Init(actionData, owner);
            actions.Add(action);
        }
    }

    public bool Update()
    {
        if (isEnd) return isEnd;
        bool isEndNow = true;
        for (int i = 0; i < actions.Count; i++)
        {
            BaseAction action = actions[i];
            isEndNow = isEndNow && action.Update();
        }
        isEnd = isEndNow;
        return isEnd;
    }
}
