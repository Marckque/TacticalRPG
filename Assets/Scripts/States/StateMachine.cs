using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State m_CurrentState;
    public virtual State CurrentState
    {
        get { return m_CurrentState; }
        set { Transition(value); }
    }

    protected bool m_IsTransitioning;

    public virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();

        if (target == null)
        {
            target = gameObject.AddComponent<T>();
        }

        return target;
    }

    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }

    protected virtual void Transition(State value)
    {
        if (CurrentState == value || m_IsTransitioning)
        {
            return;
        }

        m_IsTransitioning = true;

        if (CurrentState == null)
        {
            CurrentState.Exit();
        }

        CurrentState = value;

        if (CurrentState != null)
        {
            CurrentState.Enter();
        }

        m_IsTransitioning = false;
    }
}