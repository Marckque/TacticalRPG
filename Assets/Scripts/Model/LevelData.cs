using UnityEngine;
using System.Collections.Generic;

public class LevelData : ScriptableObject
{
    private List<Vector3> m_Tiles;
    public List<Vector3> Tiles
    {
        get { return m_Tiles; }
        set { m_Tiles = value; }
    }
}