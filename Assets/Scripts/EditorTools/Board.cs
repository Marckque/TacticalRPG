using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TilesPrefab;
    private Dictionary<Point, Tile> m_Tiles = new Dictionary<Point, Tile>();
    public Dictionary<Point, Tile> Tiles { get { return m_Tiles; } }

    public void Load(LevelData data)
    {
        for (int i = 0; i < data.Tiles.Count; ++i)
        {
            GameObject instance = Instantiate(m_TilesPrefab) as GameObject;
            Tile t = instance.GetComponent<Tile>();
            t.Load(data.Tiles[i]);
            m_Tiles.Add(t.GetPosition, t);
        }
    }
}