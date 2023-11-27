using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public bool isPlayer;
    public EnemyData enemyData;
    public Dictionary<AttrType, float> attrs = new Dictionary<AttrType, float>();
    // 属性默认值为枚举值 / 10
    public float this[AttrType type]
    {
        get
        {
            return attrs.TryGetValue(type, out float value) ? value : (int)type / 10;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            if (attrs.ContainsKey(type))
            {
                attrs[type] = value;
            }
            else
            {
                attrs.Add(type, value);
            }
        }
    }

    private bool inited = false;
    public virtual void Init(int id)
    {
        if (inited) return;
        inited = true;
        enemyData = Resources.Load<EnemyData>($"Config/Enemy_{id}");
        foreach(AttrData attr in enemyData.attrs)
        {
            attrs.Add(attr.attrType, attr.attrValue);
        }
        this[AttrType.Hp] = this[AttrType.HpMax];
    }

    public virtual void OnHurt(float damage, Entity damageSource)
    {
        this[AttrType.Hp] -= damage;
    }
}
