using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BoardEditor : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TileViewPrefab;
    [SerializeField]
    private GameObject m_TileSelectionIndicatorPrefab;

    [SerializeField]
    private int m_BoardWidth;
    [SerializeField]
    private int m_BoardHeight;
    [SerializeField]
    private int m_BoardDepth;
    [SerializeField]
    private Point m_Position;
    [SerializeField]
    private LevelData m_LevelData;

    Dictionary<Point, Tile> m_Tiles = new Dictionary<Point, Tile>();

    Transform _marker;
    Transform Marker
    {
        get
        {
            if (_marker == null)
            {
                GameObject instance = Instantiate(m_TileSelectionIndicatorPrefab);
                _marker = instance.transform;
            }

            return _marker;
        }
    }

    public void GrowArea()
    {
        Rect rect = RandomRect();
        GrowRect(rect);
    }

    public void GrowRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                GrowSingle(p);
            }
        }
    }

    public void ShrinkArea()
    {
        Rect rect = RandomRect();
        ShrinkRect(rect);
    }

    public void ShrinkRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                ShrinkSingle(p);
            }
        }
    }

    public Rect RandomRect()
    {
        int x = UnityEngine.Random.Range(0, m_BoardWidth);
        int y = UnityEngine.Random.Range(0, m_BoardDepth);
        int w = UnityEngine.Random.Range(1, m_BoardWidth - x + 1);
        int h = UnityEngine.Random.Range(0, m_BoardDepth - y + 1);
        return new Rect(x, y, w, h);
    }

    private Tile Create()
    {
        GameObject instance = Instantiate(m_TileViewPrefab);
        instance.transform.SetParent(transform);
        return instance.GetComponent<Tile>();
    }

    private Tile GetOrCreate(Point p)
    {
        if (m_Tiles.ContainsKey(p))
        {
            return m_Tiles[p];
        }

        Tile t = Create();
        t.Load(p, 0);
        m_Tiles.Add(p, t);

        return t;
    }

    private void GrowSingle(Point p)
    {
        Tile t = GetOrCreate(p);
        if (t.GetHeight < m_BoardHeight)
        {
            t.Grow();
        }
    }

    private void ShrinkSingle(Point p)
    {
        if (m_Tiles.ContainsKey(p))
        {
            return;
        }

        Tile t = m_Tiles[p];
        t.Shrink();

        if (t.GetHeight <= 0)
        {
            m_Tiles.Remove(p);
            DestroyImmediate(t.gameObject);
        }
    }

    public void Grow()
    {
        GrowSingle(m_Position);
    }

    public void Shrink()
    {
        ShrinkSingle(m_Position);
    }

    public void UpdateMarker()
    {
        Tile t = m_Tiles.ContainsKey(m_Position) ? m_Tiles[m_Position] : null;
        Marker.localPosition = t != null ? t.Center : new Vector3(m_Position.x, 0, m_Position.z);
    }

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        m_Tiles.Clear();
    }

    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
        {
            CreateSaveDirectory();
        }

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.m_Tiles = new List<Vector3>(m_Tiles.Count);
        foreach(Tile t in m_Tiles.Values)
        {
            board.m_Tiles.Add(new Vector3(t.GetPosition.x, t.GetHeight, t.GetPosition.z));
        }

        string fileName = string.Format("Assets/Resources/Levels/{1}_{2}.asset", filePath, name, Directory.GetFiles(filePath).Length.ToString());
        AssetDatabase.CreateAsset(board, fileName);
    }


    public void Load()
    {
        Clear();

        if (m_LevelData == null)
        {
            return;
        }

        foreach (Vector3 vector in m_LevelData.m_Tiles)
        {
            Tile t = Create();
            t.Load(vector);
            m_Tiles.Add(t.GetPosition, t);
        }
    }

    public void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        filePath += "/Levels";

        if (!Directory.Exists(filePath))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        }

        AssetDatabase.Refresh();
    }
}