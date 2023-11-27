using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class MenuBtnUI : MonoBehaviour
{
    private bool isSelected = false;

    public Text text_title;
    public Text text_title1;
    public Text text_desc;
    public CanvasGroup cg;

    private bool isSingleBtn;
    private float textHeight;
    private UnityAction<MenuBtnUI> action_click;
    private Tween tween_anim;

    public void Init(string title, string desc, bool isSingleBtn, UnityAction<MenuBtnUI> action_click)
    {
        // 多打几个空格，按钮触发范围大一点
        text_title.text = title;
        text_title1.text = title;
        text_desc.text = desc;
        // 与Content Size Filter 中的 PreferedSize 相同，获取应得的Height
        textHeight = LayoutUtility.GetPreferredSize(text_desc.rectTransform, 1) + 1;

        this.isSingleBtn = isSingleBtn;
        this.action_click = action_click;
        OnSelected(false);
    }

    public void OnClick()
    {
        action_click?.Invoke(this);
    }

    public void OnSelected(bool isSelected)
    {
        if (tween_anim != null && tween_anim.IsPlaying())
        {
            tween_anim.Kill();
        }
        tween_anim = DOTween.To(() => text_desc.rectTransform.sizeDelta.y, y => text_desc.rectTransform.sizeDelta = new Vector2(100, y), isSelected ? textHeight : 0, 0.5f);
        text_desc.gameObject.SetActive(isSelected);
        cg.alpha = isSelected ? 1 : 0;
    }
}
