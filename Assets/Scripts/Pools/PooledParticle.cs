using System;
using UnityEngine;

public class PooledParticle : MonoBehaviour
{
    public event Action<PooledParticle, string> OnFinished;

    private ParticleSystem part;
    public ParticleSystem Particle { get { return part; } }
    string key = string.Empty;
    public void SetKey(string _key)
    {
        key  = _key;
        part = GetComponent<ParticleSystem>();
        var main = part.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        OnFinished?.Invoke(this, key);
    }
}