using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using LitJson;

public class Manager : SingleTon<Manager>
{
    public Transform numberParent;                       //生成数字的池子

    public bool hasMove = false;                           //是否有数字发生了移动

    public int level_score = 0;                            //此次关卡分数

    public ObjectPool<Number> pool_Number = new ObjectPool<Number>(16);

    public Dialog dialog;

    public int width = 4;
    public int height = 4;
    public int maxScore = 0;

    public void PrintNow(string str = "")
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                str = str + " " + (numbers[i, j] ? numbers[i, j].num.ToString() : "e");
            }
            str = str + ", ";
        }
        print(str);
    }

    #region MovingNum
    private List<Number> isMovingNum = new List<Number>();  //正在移动中的Num
    public bool DetectMovingNumEmpty()
    {
        return isMovingNum.Count == 0;
    }

    public bool DetectMovingNumContains(Number num)
    {
        return isMovingNum.Contains(num);
    }

    public void AddNumToMoving(Number num)
    {
        if (!isMovingNum.Contains(num))
        {
            isMovingNum.Add(num);
            GameManager.Instance.ChangeGameState(GameState.ChessMoving);
        }
    }

    public void RemoveNumFromMoving(Number num)
    {
        if (isMovingNum.Contains(num))
        {
            isMovingNum.Remove(num);
            if (isMovingNum.Count == 0)
            {
                GameManager.Instance.ChangeGameState(GameState.During);
            }
        }
    }

    public void ClearNumFromMoving()
    {
        isMovingNum.Clear();
    }
    #endregion

    #region numbers
    private Number[,] numbers;                            //保存方格中的数组
    public void OnSetNumber(int posX, int posY, Number _number)
    {
        numbers[posX, posY] = _number;
    }

    public Number OnGetNumber(int posX, int posY)
    {
        return numbers[posX, posY];
    }

    public Number[,] OnGetNumbers()
    {
        return numbers;
    }
    #endregion

    public void StartChess()
    {
        int max = width * height;
        pool_Number = new ObjectPool<Number>(max);
        pool_Number.OnSetData(Resources.Load<Number>("Prefabs/Num"), numberParent, null);

        // 初始化
        numbers = new Number[width, height];
        ReStartBtn();
        LoadState();
    }

    public void LoadState()
    {
        if (JsonHelper.TryGetJsonString(out string jsonData))
        {
            JsonData data = JsonMapper.ToObject(jsonData);
            MapData map = JsonMapper.ToObject<MapData>(data["Map"].ToString());
            ClearNum();
            for (int k = 0; k < map.width; k++)
            {
                for (int j = 0; j < map.height; j++)
                {
                    if (map.numbers[k * map.width + j] > 0)
                    {
                        SetNumByIndex(k, j, map.numbers[k * map.width + j]);
                    }
                }
            }
        }
    }

    // 重新开始
    public void ReStartBtn()
    {
        ClearNumFromMoving();
        ClearNum();
        hasMove = false;
        //游戏开始生成两个数字
        CreateNum();
        CreateNum();
        //分数
        level_score = 0;
        SetScore();
        UIManager.Instance.SetMaxScore(PlayerPrefs.GetInt("HightScroe"));
        dialog?.SetMaxScore(PlayerPrefs.GetInt("HightScroe"));
    }

    private void DetectCreateNum()
    {
        if (hasMove && DetectMovingNumEmpty())   //生成新的数字
        {
            CreateNum();
            hasMove = false;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (numbers[i, j] != null)
                    {
                        numbers[i, j].OneMove = false;
                    }
                }
            }
        }
    }

    private Number CreateNum(int i = 0)
    {
        return pool_Number.New(null, i);
    }

    public void ClearNum(int num = int.MaxValue)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Number temp = numbers[i, j];
                if (temp != null && temp.num <= num)
                {
                    RemoveNumFromMoving(temp);
                    numbers[i, j] = null;
                    pool_Number.Store(temp);
                }
            }
        }
        numbers = new Number[width, height];
    }

    private void Update()
    {
        DetectCreateNum();
    }

    // 设置
    public void SetNumByIndex(int x, int y, int value)
    {
        if (value <= 0) return;
        Number num = CreateNum(1);
        numbers[x, y] = num;
        num.Init(x, y, value);
    }

    // 数字移动方法
    public void MoveNum(int directionX, int directionY)
    {
        // 下一轮 存档
        string curData = JsonHelper.GetJson();
        bool toSave = false;
        int damage = 0;
        if (GetCurNum() <= 0)
        {
            hasMove = true;
            return;
        }
        //===========向右移动==================
        if (directionX == 1)
        {
            //首先将空格填满   最右侧列不需做判断
            for (int j = 0; j < height; j++)
            {
                for (int i = width - 2; i >= 0; i--)
                {
                    if (numbers[i, j] != null)  //格子中有物体（数字），，调用移动方法
                    {
                        var save = numbers[i, j].Move(directionX, directionY, ref damage);  // || 或运算有时会让后面的表达式不运算
                        toSave = toSave || save;
                    }
                }
            }
        }
        else
        //===========向左移动==================
        if (directionX == -1)
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 1; i < width; i++)
                {   //最左侧的一列 [0,0] [0,1] [0,2] [0,3]
                    if (numbers[i, j] != null)
                    {
                        var save = numbers[i, j].Move(directionX, directionY, ref damage);
                        toSave = toSave || save;
                    }
                }
            }
        }
        else
        //===========向上移动==================
        if (directionY == 1)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = height - 2; j >= 0; j--)
                {
                    if (numbers[i, j] != null)
                    {
                        var save = numbers[i, j].Move(directionX, directionY, ref damage);
                        toSave = toSave || save;
                    }
                }
            }
        }
        else
        //===========向下移动==================
        if (directionY == -1)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 1; j < height; j++)
                {
                    if (numbers[i, j] != null)  //有物体（数字）就移动
                    {
                        var save = numbers[i, j].Move(directionX, directionY, ref damage);
                        toSave = toSave || save;
                    }
                }
            }
        }
        // SetScore();
        // 合成后的扣血，以及其他的一些回合切换，技能CD等处理
        if (damage > 0)
        {
            BattleManager.Instance.OnHurt(damage);
        }

        if (toSave)
        {
            JsonHelper.SaveJsonString(curData);
        }
    }

    public bool isFull()
    {
        return GetCurNum() >= width * height;
    }

    public int GetCurNum()
    {
        int sum = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (numbers[i, j] != null)
                {
                    sum++;
                }
            }
        }
        return sum;
    }

    // 判断是否是空格的方法
    public bool isEmpty(int x, int y)
    {
        if (isOutOfBound(x, y))
        {
            return false;
        }
        else if (numbers[x, y] != null)
        {
            return false;
        }
        return true;
    }
    public bool isEmpty(Vector2Int vec)
    {
        return isEmpty(vec.x, vec.y);
    }

    public bool isOutOfBound(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return true;
        }
        return false;
    }
    public bool isOutOfBound(Vector2Int vec)
    {
        return isOutOfBound(vec.x, vec.y);
    }

    // 判断游戏是否结束
    public bool isDead()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (numbers[i, j] == null)
                {
                    return false;
                }
            }
        }
        //===========是否能左右移动==================
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width - 1; i++)
            {
                if (numbers[i, j].num == numbers[i + 1, j].num)
                {
                    return false;
                }
            }
        }
        //===========是否能上下移动==================
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                if (numbers[i, j].num == numbers[i, j + 1].num)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // 计算得分
    public void SetScore()
    {
        int curScore = 0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (numbers[i, j])
                    curScore += numbers[i, j].num;
            }
        }
        level_score = curScore;
        UIManager.Instance.SetCurScore(curScore);
        dialog?.SetCurScore(curScore);
        if (curScore > PlayerPrefs.GetInt("HightScroe"))
        {
            PlayerPrefs.SetInt("HightScroe", curScore);
            UIManager.Instance.SetMaxScore(PlayerPrefs.GetInt("HightScroe"));
            dialog?.SetMaxScore(PlayerPrefs.GetInt("HightScroe"));
        }
    }

    public void CheckAfterTurn()
    {
        //游戏结束的UI显示，以及分数赋值
        if (isDead())
        {
            ShowFailed();
        }
    }

    public void ShowFailed()
    {
        UIManager.Instance.ShowFailed();
    }

    public void ShowSucceed()
    {
        UIManager.Instance.ShowSucceed();
    }

    public List<Color> colorList = new List<Color>();
    public Color GetColor(int value)
    {
        int lv = (int)Mathf.Log(value, 2) - 1;
        if (lv >= colorList.Count)
        {
            return colorList[colorList.Count - 1];
        }
        return colorList[lv];
    }

    public void OnSetDialog(Dialog dialog)
    {
        this.dialog = dialog;
    }

    // todo
    public Color GetBgColor()
    {
        int lv = (int)Math.Log(PlayerPrefs.GetInt("HightScroe"));
        if (lv >= colorList.Count)
        {
            return colorList[colorList.Count - 1];
        }
        return colorList[lv];
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        //if (GUI.Button(new Rect(50, 50, 150, 35), "调试"))
        //{
        //    PrintNow($"调试 {GetCurNum()} : ");
        //}
    }
#endif
}