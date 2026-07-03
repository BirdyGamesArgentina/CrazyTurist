using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class InterestPoint : MonoBehaviour
{
    [Header("Unity Events")]
    public UnityEvent _onEnter;
    public UnityEvent _onExit;
    public UnityEvent _onFinishVisit;

    [Header("Interest Point Info")]
    public IP_ScriptableObject myMonument;

    public void Enter()
    {
        _onEnter.Invoke();
        OnEnter();
    }
    public void Exit()
    {
        _onExit.Invoke();
        OnExit();
    }
    protected void FinishVisit()
    {
        _onFinishVisit.Invoke();
        OnFinishVisit();
    }

    protected abstract void OnEnter();
    protected abstract void OnExit();
    protected abstract void OnFinishVisit();

}
