using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : BaseAction
{
    protected override void Execute()
    {
        base.Execute();
        BattleManager.Instance.OnHurt(actionData.numerical1, owner.isPlayer);
    }
}
