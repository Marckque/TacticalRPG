using UnityEngine;

public class BattleController : StateMachine
{
    public CameraController m_CameraController;
    public Board m_Board;
    public LevelData m_LevelData;
    public Transform tileSelection;
    public Point pos;

    private void Start()
    {
        ChangeState<InitialiseBattleState>();
    }
}