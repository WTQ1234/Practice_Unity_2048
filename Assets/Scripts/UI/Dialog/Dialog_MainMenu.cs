using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class Dialog_MainMenu : Dialog
{
    public GameObject root_btns;
    public GameObject root_imgs;

    private List<MenuBtnUI> btns;
    public List<Image> imgs;

    public MenuBtnUI curBtn;
    private CanvasGroup cg_imgs;
    private Tween tween_alpha;

    public int curIndex;

    private void Start()
    {
        curIndex = -1;
        cg_imgs = root_imgs.GetComponent<CanvasGroup>();
        imgs = new List<Image>(root_imgs.GetComponentsInChildren<Image>());
        OnSetImgShow(0);
        btns = new List<MenuBtnUI>(root_btns.GetComponentsInChildren<MenuBtnUI>());
        btns[0].Init("禅模式", " — 常规的2048机制 \n — 无敌人、回合、道具 \n — 可以随意使用撤回 \n — 来晚风中悠闲散步吧~",
            false, OnClickBtn0);
        btns[1].Init("故事模式", " — 合成数字进行战斗 \n — 使用道具攻克难关 \n — 加快速度，雨会越下越大 \n — 来细雨中追逐落叶吧~",
            false, OnClickBtn1);
        btns[2].Init("无尽模式", " — 击退敌人，选择强化 \n — 合成4096，重置棋盘 \n — 来暴雨中驾驭狂风吧~",
            false, OnClickBtn2);
        btns[3].Init("设置", "", true, OnClickBtn3);
        btns[4].Init("致谢", "", true, OnClickBtn4);
    }

    private void OnClickBtn(MenuBtnUI btn, int index)
    {
        print($"On Click {index} cur:{curIndex}");
        if (index == curIndex) return;
        curIndex = index;
        curBtn?.OnSelected(false);
        curBtn = btn;
        curBtn?.OnSelected(true);
        tween_alpha = DOTween.To(() => cg_imgs.alpha, x => cg_imgs.alpha = x, 0, 0.3f);//血量变化
        tween_alpha.onComplete = () => {
            OnSetImgShow(index);
            tween_alpha = DOTween.To(() => cg_imgs.alpha, x => cg_imgs.alpha = x, 1, 0.3f);//血量变化
        };
    }

    private void OnClickBtn0(MenuBtnUI btn)
    {
        OnClickBtn(btn, 0);
    }
    private void OnClickBtn1(MenuBtnUI btn)
    {
        OnClickBtn(btn, 1);
    }
    private void OnClickBtn2(MenuBtnUI btn)
    {
        OnClickBtn(btn, 2);
    }
    private void OnClickBtn3(MenuBtnUI btn)
    {
        OnClickBtn(btn, 3);
    }
    private void OnClickBtn4(MenuBtnUI btn)
    {
        OnClickBtn(btn, 4);
    }

    private void OnSetImgShow(int index)
    {
        for (int i = 0; i < imgs.Count; i++)
        {
            var img = imgs[i];
            img.gameObject.SetActive(i == index);
        }
    }
}
