using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject go;

    [Range(0.01f, 1f)]
    public float height = 0.5f;

    public int width;
    public int depth;
    private List<GameObject> gos = new List<GameObject>();

    public void UpdateBoard()
    {
        gos.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                GameObject g = Instantiate(go);
                g.transform.position = new Vector3(x, 0f, z);
                g.transform.localScale = new Vector3(1f, height, 1f);
                g.transform.SetParent(transform);
                gos.Add(g);
            }
        }
    }

   
}