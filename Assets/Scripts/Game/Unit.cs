using UnityEngine;
using System.Collections.Generic;

#region Comparers
// x-y → Growing order;
// y-x → Decreasing order;

public class UnitSpeedComparer : IComparer<Unit>
{
    public int Compare(Unit x, Unit y)
    {
        return y.GetSpeed() - x.GetSpeed();
    }
}

public class UnitPhysicalAttackComparer : IComparer<Unit>
{
    public int Compare(Unit x, Unit y)
    {
        return y.GetPhysicalAttack() - x.GetPhysicalAttack();
    }
}

public class UnitMagicalAttackComparer : IComparer<Unit>
{
    public int Compare(Unit x, Unit y)
    {
        return y.GetPhysicalAttack() - x.GetPhysicalAttack();
    }
}
#endregion Comparers

[System.Serializable]
public class UnitParameters
{
    public int health = 100;
    public int mana = 100;
    public int move = 4;
    public int jump = 2;
    public int physicalAttack = 20;
    public int physicalDefense = 10;
    public int magicalAttack = 20;
    public int magicalDefense = 10;
    public int speed = 50;
}

public class Unit : MonoBehaviour
{
    [SerializeField]
    private UnitParameters m_UnitParameters;

    [Header("UI"), SerializeField]
    private UnitMenu m_Menu;
    [SerializeField]
    private TextMesh[] m_TemporaryUI;

    public bool CurrentTurn { get; set; }

    private void Start()
    {
        DebugUI();
    }

    public int GetSpeed()
    {
        return m_UnitParameters.speed;
    }

    public int GetPhysicalAttack()
    {
        return m_UnitParameters.physicalAttack;
    }

    public int GetMagicalAttack()
    {
        return m_UnitParameters.magicalAttack;
    }

    private void DebugUI()
    {
        // Health
        m_TemporaryUI[0].text = m_UnitParameters.health + " HP";

        // Speed
        m_TemporaryUI[1].text = m_UnitParameters.speed + " SPD";
    }

    private void DisplayMenu()
    {
        m_Menu.Activate();
    }

    private void SetCurrentTurn()
    {
        DisplayMenu();
    }
}