using System;
using UnityEngine;

public class InfoEventArgs<T> : EventArgs
{
    private T m_Info;
    public T Info { get { return m_Info; } }

    public InfoEventArgs()
    {
        m_Info = default(T);
    }

    public InfoEventArgs(T info)
    {
        m_Info = info;
    }
}