using UnityEngine;
using System.Collections.Generic;

public class GameManagement : MonoBehaviour
{
    [Header("Camera"), SerializeField]
    private Camera m_Camera;

    [Header("Units"), SerializeField]
    private List<Unit> m_Units = new List<Unit>();
    private List<Unit> m_UnitsSpeedSort = new List<Unit>();
    private List<Unit> m_UnitsPhysicalAttackSort = new List<Unit>();
    private List<Unit> m_UnitsMagicalAttackSort = new List<Unit>();

    private int m_CurrentTurn;
    private bool m_IsPlaying;

    #region Initialise
    protected void Start()
    {
        SortAnyTypeWithComparer(m_Units, m_UnitsSpeedSort, new UnitSpeedComparer());
    }

    private void SortAnyTypeWithComparer<T>(List<T> originList, List<T> targetList, IComparer<T> comparer)
    {
        foreach (T type in originList)
        {
            targetList.Add(type);
        }

        targetList.Sort(comparer);
    }
    #endregion Initialise

    private void Update()
    {
        if (!m_IsPlaying)
        {
            SetTurn();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTurn();
        }
    }

    private void SetTurn()
    {
        m_IsPlaying = true;

        m_Units[m_CurrentTurn].CurrentTurn = true;

        m_Camera.transform.position = new Vector3(m_Units[m_CurrentTurn].transform.position.x, m_Camera.transform.position.y, m_Camera.transform.position.z);
        m_Camera.transform.LookAt(m_Units[m_CurrentTurn].transform);
    }

    private void NextTurn()
    {
        m_Units[m_CurrentTurn].CurrentTurn = false;

        if (m_CurrentTurn < m_Units.Count - 1)
        {
            m_CurrentTurn++;
        }        
        else
        {
            m_CurrentTurn = 0;
        }

        m_IsPlaying = false;
    }
}