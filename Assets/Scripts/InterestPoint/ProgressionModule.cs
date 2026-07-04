using System;
using UnityEngine;

public class ProgressionModule : MonoBehaviour
{
    [Header("Tiempo de llenado")]
    [SerializeField] private float fillTime = 3f;

    [Header("Tiempo de vaciado")]
    [SerializeField] private float drainTime = 2f;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private LerpedObject[] anims_lerped;

    private float progress = 0f;

    private bool filling = false;
    private bool draining = false;

    public bool finished = false;

    Action onFinish = delegate { };

    public void SetCallbackOnFinish(Action callback)
    {
        onFinish = callback;
    }

    public void Begin()
    {
        if (finished) return;

        filling = true;
        draining = false;
    }

    public void End()
    {
        if (finished) return;

        filling = false;
        draining = true;
    }

    public void Stop()
    {
        filling = false;
        draining = false;
    }

    private void Update()
    {
        if (finished)
            return;

        if (filling)
        {
            progress += Time.deltaTime / fillTime;
        }
        else if (draining)
        {
            progress -= Time.deltaTime / drainTime;
        }

        progress = Mathf.Clamp01(progress);

        float value = curve.Evaluate(progress);

        for (int i = 0; i < anims_lerped.Length; i++)
            anims_lerped[i].OnLerp(value);

        if (progress >= 1f)
        {
            finished = true;
            filling = false;
            draining = false;

            onFinish?.Invoke();
        }
    }
}