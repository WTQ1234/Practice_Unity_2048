using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool inputActive = true;

    public void OnSetInputActive(bool inputActive = true)
    {
        this.inputActive = inputActive;
    }

    void Update()
    {
        // 触屏 有触摸点，且滑动
        if (GameManager.Instance.CheckGameState(GameState.During))
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                int dieX = 0;
                int dieY = 0;
                //获取滑动的距离
                bool toMove = false;
                Vector2 touchDelPos = Input.GetTouch(0).deltaPosition;
                if (Mathf.Abs(touchDelPos.x) > Mathf.Abs(touchDelPos.y))
                {
                    //滑动距离
                    if (touchDelPos.x > 10)
                    {
                        toMove = true;
                        dieX = 1;
                    }
                    else
                    if (touchDelPos.x < -10)
                    {
                        toMove = true;
                        dieX = -1;
                    }
                }
                else
                {
                    if (touchDelPos.y > 10)
                    {
                        toMove = true;
                        dieY = 1;
                    }
                    else if (touchDelPos.y < -10)
                    {
                        toMove = true;
                        dieY = -1;
                    }
                }
                if (toMove)
                {
                    DoInput(dieX, dieY);
                }
            }
        }

        // PC
        if (GameManager.Instance.CheckGameState(GameState.During))
        {
            int dieX = 0;
            int dieY = 0;
            bool toMove = true;
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dieX = -1;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                dieX = 1;
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                dieY = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                dieY = -1;
            }
            else
            {
                toMove = false;
            }
            if (toMove)
            {
                DoInput(dieX, dieY);
            }
        }
    }

    private bool CheckGameState(GameState needState)
    {
        return GameManager.Instance.CheckGameState(needState);
    }

    private void DoInput(int dirX, int dirY)
    {
        Manager.Instance.MoveNum(dirX, dirY);
        Manager.Instance.SetScore();
    }
}
