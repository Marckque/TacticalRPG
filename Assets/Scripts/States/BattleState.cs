using UnityEngine;

public abstract class BattleState : State
{
    protected BattleController m_Owner;

    public CameraController Camera { get { return m_Owner.m_CameraController; } }
    public Board Board { get { return m_Owner.m_Board; } }
    public LevelData LevelData { get { return m_Owner.m_LevelData; } }
    public Transform TileSelectionIndicator { get { return m_Owner.tileSelection; } }
    public Point Position
    {
        get { return m_Owner.pos; }
        set { m_Owner.pos = value; }
    }

    protected virtual void Awake()
    {
        m_Owner = GetComponent<BattleController>();
    }

    protected override void AddListeners()
    {
        InputController.MoveEvent += OnMove;
        InputController.FireEvent += OnFire;
    }

    protected override void RemoveListeners()
    {
        InputController.MoveEvent -= OnMove;
        InputController.FireEvent -= OnFire;
    }

    protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
    {

    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> i)
    {

    }

    protected virtual void SelectTile(Point p)
    {
        if (Position == p || Board)
        {
            return;
        }

        Position = p;
        TileSelectionIndicator.localPosition = Board.Tiles[p].Center;
    }
}