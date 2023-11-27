using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BloodUI : MonoBehaviour
{
    public Image skillIcon;

    public Slider hpSlider;
    public Text text_hpNum;
    public Image hpSprite;
    public Image hpBgSprite;

    public Image cdSlider;
    public Text text_skillCD;

    public int curBloodSpriteIdx = -999;

    private Tween tween;

    // 暂定每100点1条血
    public void OnSetHp(float num, bool isInit = false)
    {
        int hpNum = (int)(num / 100);
        if (num % 100 != 0)
        {
            hpNum++;
        }
        float hpNum2 = 100 + (num - hpNum * 100); // 举例 370点血 4条血 再扣除30点血。计算得到-30，再+100，得到70点血
        DebugHelper.Instance.Log($"Set Blood UI 总血量:{num} 血条数:{hpNum} 当前显示:{hpNum2}");
        if (num <= 0)
        {
            hpNum = 0;
            hpNum2 = 0;
            UIManager.Instance.ShowSucceed();
        }
        text_hpNum.text = $"x {hpNum}";
        bool isColorChange = SetBloodColor(hpNum);
        SetBloodValue(hpNum2 / 100, isColorChange && (!isInit));    // 初始化时即会修改血条颜色，所以此时无需置零
    }

    public void OnSetCD(float num, float maxNum)
    {
        cdSlider.fillAmount = num / maxNum;
        text_skillCD.text = ((int)(maxNum - num) + 1).ToString();
    }

    private bool SetBloodColor(int hpNum)
    {
        // 血条图片由0开始，100点生命以下相除为0，再按血条最大数量取余
        int max = 12;
        // 如果传进来的是负数
        int curNum = (hpNum - 1) % max;
        if (curNum < 0)
        {
            curNum = -1;
        }
        if (curBloodSpriteIdx != curNum)
        {
            curBloodSpriteIdx = curNum;
            Sprite sprite = Resources.Load<Sprite>($"Sprite/UI/Blood/{curNum}");
            hpSprite.sprite = sprite;
            int bg = curNum - 1;
            if (bg < 0)
            {
                bg = -1;
            }
            Sprite spriteBg = Resources.Load<Sprite>($"Sprite/UI/Blood/{bg}");
            hpBgSprite.sprite = spriteBg;
            return true;
        }
        return false;
    }

    private void SetBloodValue(float num, bool change)
    {
        if (tween != null && tween.IsPlaying())
        {
            tween.Kill();
        }
        if (change)
        {
            // 2个tween嵌套
            tween = DOTween.To(() => hpSlider.value, x => hpSlider.value = x, 0, 0.3f);//血量变化
            tween.onComplete = () => {
                hpSlider.value = 1;
                tween = DOTween.To(() => hpSlider.value, x => hpSlider.value = x, num, 0.3f);//血量变化
            };
        }
        else
        {
            // 1个tween直接改
            tween = DOTween.To(() => hpSlider.value, x => hpSlider.value = x, num, 0.3f);//血量变化
        }
    }
}
