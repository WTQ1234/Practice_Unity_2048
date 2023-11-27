using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;
using DG.Tweening;

[SerializeField]
public class Number : MonoBehaviour, IResetable
{
    private Manager _manager;
    private Manager manager
    {
        get
        {
            if (_manager != null)
            {
                return _manager;
            }
            else
            {
                _manager = Manager.Instance;
                return _manager;
            }
        }
    }

    public int posX;
    public int posY;

    public int offsetX = -620;
    public int offsetY = -620;
    public int space = 420;

    private bool isMoving = false;   // 动画播放过
    private bool toDestroy;          // 判断是否销毁
    public bool OneMove = false;     // 标识是否合并过一次

    private bool isSuccess = false;

    public int num = 2;
    private Text text_value;
    private Image img_color;

    private Tweener tweener;

    public void onPopObj(int i)
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 1.6f;

        num = Random.value > 0.2f ? 2 : 4;  // 80% 2的概率
        int width = manager.width;
        int height = manager.height;
        SetTextValue();
        // 若为0，随机一个位置
        if (i == 0)
        {
            do
            {
                posX = Random.Range(0, width);
                posY = Random.Range(0, height);
                // print("detect " + posX + " " + posY + " " + (manager.numbers[posX, posY] == null) + " " + manager.isFull());
            } while (!manager.isFull() && !manager.isEmpty(posX, posY));
            ResetPos();
            manager.OnSetNumber(posX, posY, this);   //存放数字本身到数组中，表示此位置有数字不能生成新的数字
        }

        manager.CheckAfterTurn();
    }
    public void onStroeObj()
    {
        toDestroy = false;
        if (tweener != null && tweener.IsPlaying())
        {
            tweener.Kill();
        }
        gameObject.SetActive(false);
    }

    public void Init(int x, int y, int value)
    {
        posX = x;
        posY = y;
        num = value;
        SetTextValue();
        ResetPos();
    }

    private void Awake()
    {
        img_color = transform.Find("Color").GetComponent<Image>();
        text_value = transform.Find("Color/Text").GetComponent<Text>();
    }

    void Update()
    {
        //播放一次动画
        if (!isMoving)
        {
            if (transform.localPosition != GetLocalPos())
            {
                isMoving = true;
                // print((GetLocalPos() - transform.localPosition).magnitude);
                if (tweener != null && tweener.IsPlaying())
                {
                    tweener.Kill();
                }
                // tweener = transform.DOLocalMove(GetLocalPos(), (GetLocalPos() - transform.localPosition).magnitude * 0.0005f);
                tweener = transform.DOLocalMove(GetLocalPos(), 0.3f);
                tweener.onComplete = ()=>{ MoveOver();};
            }
        }
    }

    // todo 把合成的格子填入全局列表，以从位置处播放特效
    public bool Move(int directionX, int directionY, ref int damage)
    {
        Vector2Int vec = new Vector2Int(directionX, directionY);
        Vector2Int curVec = new Vector2Int(posX, posY);
        bool isChange = false;
        while(manager.isEmpty(curVec + vec))
        {
            curVec += vec;
            isChange = true;
        }
        //有空格的移动
        if (isChange)
        {
            if (!manager.DetectMovingNumContains(this))
            {
                //保证不会重复添加物体（数字）到列表，
                manager.AddNumToMoving(this);
            }
            //移动一次，就生成两个数字的标志符
            manager.hasMove = true;
            //向空格位置移动
            manager.OnSetNumber(posX, posY, null);
            posX = curVec.x;
            posY = curVec.y;
            manager.OnSetNumber(posX, posY, this);
        }
        // 检测是否有可合并的格子
        Vector2Int nextVec = curVec + vec;
        int nextX = nextVec.x;
        int nextY = nextVec.y;
        if (nextX >= 0 && nextX < manager.width && nextY >= 0 && nextY < manager.height)
        {
            Number temp = manager.OnGetNumber(nextX, nextY);
            if (temp != null && num == temp.num && !temp.OneMove)
            {
                //只和并一次的标志
                temp.OneMove = true;
                //移动的标志，（生成新的物体（数字））
                manager.hasMove = true;
                //动画播放的限定（有数字在列表中就不会重复播放第二次动画）
                if (!manager.DetectMovingNumContains(this))   //不会重复添加物体（数字）到列表，
                {
                    manager.AddNumToMoving(this);
                }
                //碰到一样的数字，讲位置设为空 并销毁本身标识（true），再将其位置上的值变为2倍，（更换成新的数字）
                toDestroy = true;
                manager.OnSetNumber(posX, posY, null);
                // 重设位置
                posX = nextX;
                posY = nextY;
                damage += temp.num;  // 合成造成的伤害值
                temp.num *= 2;
                //游戏成功
                isSuccess = temp.num == 4096;
                return true;
            }
        }
        return isChange;
    }

    // 动画结束，标志改为false
    public void MoveOver()
    {
        isMoving = false;
        if (toDestroy)   //若碰到了相同的数字  销毁自己，和改变另一个图片（数字）
        {
            manager.pool_Number.Store(this);
            // 移动完成后才更新目标值
            manager.OnGetNumber(posX, posY).SetTextValue();
            //游戏成功
            if (isSuccess)
            {
                manager.ShowSucceed();
            }
        }
        manager.RemoveNumFromMoving(this);
    }

    public void SetTextValue()
    {
        if (text_value != null)
        {
            text_value.text = num.ToString();
        }
        img_color.color = manager.GetColor(num);
    }

    public void ResetPos()
    {
        transform.localPosition = GetLocalPos();
    }

    public Vector3 GetLocalPos()
    {
       return new Vector3(offsetX + posX * space, offsetY + posY * space, 0);
    }
}
