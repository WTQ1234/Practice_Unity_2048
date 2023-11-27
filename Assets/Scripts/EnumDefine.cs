public enum SkillType
{
    Attack = 1,         // 普攻
    CriticalAtk = 2,    // 暴击
    Heal = 3,           // 治疗
    Buff = 4,
    DeBuff = 5,
    ReSortMap = 6,
    LockMap = 7,
}

// 默认值为枚举对应数字 / 10
public enum AttrType
{
    Critical = 01,
    Def = 02,        // 防御-减法公式
    Sheild = 03,
    Rage = 04,       // 怒气

    Hp = 10,
    HpMax = 11,
    Atk = 12,
    CDComboTime = 13,// 每次Combo的延迟时间
    CDRate = 14,     // 敌人的CD加速减速

    CDTime = 21,
}

public enum GameState
{
    Before,         // 还未开始，剧情，选择等
    During,         // 游戏进行中
    ChessMoving,    // 棋子移动中
    PlayerSkill,    // 玩家释放技能，冻结CD和操作
    EnemySkill,     // 敌人释放技能，冻结CD和操作
    RainChange,     // 难度切换
    End,            // 游戏结束
}