using System;
using UnityEngine;

class Repeater
{
    private const float THRESHOLD = 0.5f;
    private const float RATE = 0.25f;
    private bool m_Hold;
    private float m_Next;
    private string m_Axis;

    public Repeater(string axisName)
    {
        m_Axis = axisName;
    }

    public int Update()
    {
        int returnValue = 0;
        int value = Mathf.RoundToInt(Input.GetAxisRaw(m_Axis));

        if (value != 0)
        {
            if (Time.time > m_Next)
            {
                returnValue = value;
                m_Next = Time.time + (m_Hold ? RATE : THRESHOLD);
                m_Hold = true;
            }
        }
        else
        {
            m_Hold = false;
            m_Next = 0;
        }

        return returnValue;
    }
}

public class InputController : MonoBehaviour
{
    public static event EventHandler<InfoEventArgs<Point>> MoveEvent;
    public static event EventHandler<InfoEventArgs<int>> FireEvent;

    private Repeater m_HorizontalInput = new Repeater("Horizontal");
    private Repeater m_VerticalInput = new Repeater("Vertical");
    private string[] m_Buttons = new string[] { "Fire1", "Fire2", "Fire3" };

    void Update()
    {
        int x = m_HorizontalInput.Update();
        int y = m_VerticalInput.Update();

        if (x != 0 || y != 0)
        {
            if (MoveEvent != null)
            {
                MoveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
            }
        }

        for (int i = 0; i < 3; ++i)
        {
            if (Input.GetButtonUp(m_Buttons[i]))
            {
                if (FireEvent != null)
                {
                    FireEvent(this, new InfoEventArgs<int>(i));
                }
            }
        }
    }
}