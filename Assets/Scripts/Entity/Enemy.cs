using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public BloodUI bloodUI;
    public Animator animator;

    public Skill skill;
    public bool isSkillMoving
    {
        get
        {
            return skill != null;
        }
    }

    private float curTime = 0f;
    private int curSkillIndex = 0;

    public override void Init(int id)
    {
        base.Init(id);
        isPlayer = false;
        bloodUI.OnSetHp(this[AttrType.Hp], true);
    }

    private void LateUpdate()
    {
        // 如果当前有技能正在释放，就过技能的时间轴，否则过技能的冷缩条
        if (skill != null)
        {
            bool isEnd = skill.Update();
            if (isEnd)
            {
                skill = null;
                OnEndSkill();
            }
        }
        else
        {
            if (!GameManager.Instance.CheckGameState(GameState.During)) return;
            curTime += Time.deltaTime;
            if (curTime > this[AttrType.CDTime] * this[AttrType.CDRate])
            {
                // Debug.Log($"{curTime} {this[AttrType.CDTime]} {this[AttrType.CDRate]} 执行一次技能");
                curTime = 0;
                UseSkill();
            }
            bloodUI.OnSetCD(curTime, this[AttrType.CDTime] * this[AttrType.CDRate]);
        }
    }

    private void UseSkill()
    {
        skill = new Skill(enemyData.skills[curSkillIndex], this);

        curSkillIndex++;
        if (curSkillIndex >= enemyData.skills.Count)
        {
            curSkillIndex = 0;
        }
        Debug.Log("1");
        // Skill 这里踩了一个坑记录一下，在Animator里面从状态切换到另一个状态需要时间过渡，而Skill时间太短，会在短时间内播放两次，可在其Animator里修改过渡时间
        animator.SetTrigger("Skill"); // todo 后续专门做一个Action播放动画
        OnUseSkill();
    }

    public override void OnHurt(float damage, Entity damageSource)
    {
        base.OnHurt(damage, damageSource);
        if (damage != 0)
        {
            DebugHelper.Instance.Log($"Enemy OnHurt {damage}");
            curTime -= this[AttrType.CDComboTime];
            curTime = Mathf.Max(curTime, 0);
            bloodUI.OnSetHp(this[AttrType.Hp]);
            bloodUI.OnSetCD(curTime, this[AttrType.CDTime] * this[AttrType.CDRate]);
            float blend = Mathf.Clamp(Mathf.Clamp(Mathf.Sqrt(damage), 2, 6) / 6, 0, 1);
            animator.SetTrigger("OnHurt"); // todo 后续专门做一个Action播放动画
            animator.SetFloat("Blend", blend); // todo 后续专门做一个Action播放动画
        }
    }

    private void OnUseSkill()
    {
        GameManager.Instance.ChangeGameState(GameState.EnemySkill);
    }
    private void OnEndSkill()
    {
        GameManager.Instance.ChangeGameState(GameState.During);
    }
}
