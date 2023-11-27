using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider hpSlider;
    public Slider rangeSlider;
    // public Image skillIcon;
    // public Text text_skillCD;
    // public Text text_hpNum;

    // 暂定每100点1条血
    public void OnSetHp(float hp, float hpMax = 100)
    {
        hpSlider.value = hp / hpMax;
        if (hp <= 0)
        {
            UIManager.Instance.ShowFailed();
        }
        // int hpNum = (int)(hp / hpMax);
        // if (num % 100 != 0)
        // {
        //     hpNum++;
        // }
        // float hpNum2 = 100 + (num - hpNum * 100); // 举例 370点血 4条血 再扣除30点血。计算得到-30，再+100，得到70点血
        // print(num + "  " +  hpNum + " " + hpNum2);
        // if (num <= 0)
        // {
        //     hpNum = 0;
        //     hpNum2 = 0;
        // }
        // hpSlider.value = hpNum2 / 100;
        // text_hpNum.text = $"x {hpNum}";
    }

    public void OnSetRange(float num)
    {
        rangeSlider.value = num / 100;
    }
}
