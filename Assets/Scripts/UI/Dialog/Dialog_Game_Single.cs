using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Game_Single : Dialog
{
    public Text text_cur;                                 //当前关卡显示
    public Text text_max;                               //最高分数

    public GameObject Menu;
    public GameObject UIFinsh;                             //游戏结束页面
    public GameObject UISucceed;                           //游戏成功页面

    public Button btn_back;
    public Button btn_skill;
    public Button btn_restart;
    public Button btn_showMenu;
    public Button btn_restart1;
    public Button btn_restart2;

    private void Awake()
    {
        DebugHelper.Instance.Log($"当前存档位置 {Application.persistentDataPath}");
        ChangeMenuState();
        btn_back.onClick.AddListener(Back);
        btn_skill.onClick.AddListener(Skill);
        btn_restart.onClick.AddListener(ReStartBtn);
        btn_restart1.onClick.AddListener(ReStartBtn);
        btn_restart2.onClick.AddListener(ReStartBtn);
        btn_showMenu.onClick.AddListener(ChangeMenuState);
    }

    public void ReStartBtn()
    {
        Manager.Instance.ReStartBtn();
        UIFinsh.SetActive(false);
        text_cur.text = "0";
        if (PlayerPrefs.HasKey("HightScroe"))
        {
            text_max.text = PlayerPrefs.GetInt("HightScroe").ToString();
        }
        else
        {
            text_max.text = "0";
        }
    }

    // 显示游戏成功
    public void ShowSucceed()
    {
        UISucceed.SetActive(true);
    }

    // 显示游戏失败
    public void ShowFailed()
    {
        UIFinsh.SetActive(true);
    }

    // 撤回一步
    public void Back()
    {
        // todo 撤回这里需要检测游戏状态，提出作为技能
        if (Manager.Instance.DetectMovingNumEmpty())
        {
            Manager.Instance.LoadState();
        }
    }

    // 退出应用
    public void QuitApp()
    {
        Application.Quit();
    }

    // 复活
    void Skill()
    {
        UIFinsh.SetActive(false);
        Manager.Instance.ClearNum(16);
    }

    // 菜单
    private bool menuShow = true;
    public void ChangeMenuState()
    {
        menuShow = !menuShow;
        Menu.SetActive(menuShow);
    }

    public void SetCurScore(int score)
    {
        text_cur.text = score.ToString();
    }

    public void SetMaxScore(int max)
    {
        text_max.text = max.ToString();
    }
}
