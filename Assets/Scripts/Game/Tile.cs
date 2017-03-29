using UnityEngine;

public class Tile : MonoBehaviour
{
    private const float HEIGHT_MULTIPLIER = 0.5f;

    [SerializeField]
    private Point m_Position;
    [SerializeField]
    private int m_CurrentHeight;

    public int GetHeight { get { return m_CurrentHeight; } }
    public Point GetPosition { get { return m_Position; } }
    public Vector3 Center { get { return new Vector3(m_Position.x, m_CurrentHeight * HEIGHT_MULTIPLIER, m_Position.z); } }    

    #region Load method
    public void Load(Point p, int h)
    {
        m_Position = p;
        m_CurrentHeight = h;

        Match();
    }

    public void Load(Vector3 vector)
    {
        Load(new Point((int)vector.x, (int)vector.z), (int)vector.y);
    }
    #endregion Load method

    public void Grow()
    {
        m_CurrentHeight++;
        Match();
    }

    public void Shrink()
    {
        if (m_CurrentHeight > 0)
        {
            m_CurrentHeight--;
            Match();
        }
    }

    private void Match()
    {
        UpdatePosition();
        UpdateScale();
    }

    private void UpdatePosition()
    {
        transform.localPosition = new Vector3(m_Position.x, m_CurrentHeight * HEIGHT_MULTIPLIER, m_Position.z);
    }

    private void UpdateScale()
    {
        transform.localScale = new Vector3(1, m_CurrentHeight * HEIGHT_MULTIPLIER, 1);
    }
}