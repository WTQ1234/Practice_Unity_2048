using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SingleTon<BattleManager>
{
    public float water = 0f;
    public float rainSpeed = 0.5f;
    public int rainLevel = 1;
    public Enemy enemy;
    public Player player;

    // 下一回合 计算伤害，冷却和技能和下雨
    private void Start()
    {
        InitEnemy(1);
        InitPlayer(0);
    }

    public void InitPlayer(int id)
    {
        player.Init(id);
    }
    public void InitEnemy(int id)
    {
        enemy.Init(id);
    }

    public void OnHurt(float damage, bool isPlayer = true)
    {
        // 受攻击者
        Entity entity1 = isPlayer ? enemy as Entity : player as Entity;
        // 攻击者
        Entity entity2 = isPlayer ? player as Entity : enemy as Entity;
        entity1.OnHurt(damage, entity2);
    }
}
