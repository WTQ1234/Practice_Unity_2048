using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public PlayerUI playerUI;
    public Animator animator;

    public override void Init(int id)
    {
        base.Init(id);
        isPlayer = true;
    }

    public override void OnHurt(float damage, Entity damageSource)
    {
        base.OnHurt(damage, damageSource);
        if (damage != 0)
        {
            DebugHelper.Instance.Log($"Player OnHurt {damage}");
            playerUI.OnSetHp(this[AttrType.Hp], this[AttrType.HpMax]);
            animator.Play("Anim_Hero_Attack");
        }
    }

    public void OnChangeRange(float rage)
    {
        float curRage = this[AttrType.Rage] + rage;
        playerUI.OnSetRange(curRage);
    }
}
