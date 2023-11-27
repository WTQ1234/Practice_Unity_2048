using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    private GameState state
    {
        get
        {
            return fsmComponent.GetFSMType();
        }
    }
    public FSMComponent<GameState> fsmComponent;

    private void Start()
    {
        // todo 用作泛型的类好像不能挂在编辑模式？
        fsmComponent = new FSMComponent<GameState>();
        fsmComponent.SetFSM(new FSMData<GameState>(GameState.Before, new (GameState, GameState[], string)[] {
            (GameState.Before, new GameState[] {GameState.During}, default),
            (GameState.During, new GameState[] {GameState.ChessMoving, GameState.PlayerSkill, GameState.EnemySkill, GameState.RainChange, GameState.End}, default),
            (GameState.ChessMoving, new GameState[] {GameState.During, GameState.End}, default),
            (GameState.PlayerSkill, new GameState[] {GameState.During, GameState.End}, default),    // todo
            (GameState.EnemySkill, new GameState[] {GameState.During, GameState.End}, default),
            (GameState.RainChange, new GameState[] {GameState.During, GameState.End}, default),     // todo
            (GameState.End, new GameState[] {GameState.Before, GameState.During}, default),
        }));
        // todo 后续出专门的主界面进行调用
        StartGame();
    }

    public bool CheckGameState(GameState state)
    {
        return state == fsmComponent.GetFSMType();
    }

    public void ChangeGameState(GameState state)
    {
        fsmComponent.ChangeState(state);
    }

    public void ForceGameState(GameState state)
    {
        fsmComponent.ForceState(state);
    }

    public void StartGame()
    {
        ChangeGameState(GameState.During);
        Manager.Instance.StartChess();
    }
}
