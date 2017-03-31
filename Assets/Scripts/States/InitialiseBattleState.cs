using UnityEngine;
using System.Collections;

public class InitialiseBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        Board.Load(LevelData);
        Point p = new Point((int)LevelData.Tiles[0].x, (int)LevelData.Tiles[0].z);
        SelectTile(p);
        yield return null;
        m_Owner.ChangeState<MoveTargetState>();
    }
}