using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionsUI : MonoBehaviour
{
    public Text text_Name;
    public Image image_Bg;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;

    public AudioSource audioSource;
    public Animator animator;

    private TransitionData transitionData;

    float curTime = 0f;
    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > 0.5f)
        {
            curTime = 0;
            OnSetTrans(1);
        }
    }

    public void OnSetTrans(int id)
    {
        transitionData = Resources.Load<TransitionData>($"Config/Transition_{id}");

        text_Name.text = transitionData.Name;
        image_Bg.color = transitionData.color;
        image1.sprite = transitionData.sprite1;
        image2.sprite = transitionData.sprite2;
        image3.sprite = transitionData.sprite3;
        image4.sprite = transitionData.sprite4;
        image5.sprite = transitionData.sprite5;

        // todo 播放UI动画结束的时候，给一个Invoke，考虑用动画事件来做
        animator.Play("Anim_TransitionsUI");
    }
}
