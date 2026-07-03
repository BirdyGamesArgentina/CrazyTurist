using System;
using UnityEngine;

public class ProgressionModule : MonoBehaviour
{
    [Header("Tiempo de Completado")]
    [SerializeField] float time_to_trans = 1f;
    float timer_trans = 1f;
    bool anim = false;
    
    [SerializeField] AnimationCurve curve;
    [SerializeField] LerpedObject[] anims_lerped; // ejecuta el OnLerp(float)

    bool firstTime = true;
    bool finished = false;

    Action onFinish = delegate { };
    public void SetCallbackOnFinish(Action _onFinish)
    {
        onFinish = _onFinish;
    }
    public void Begin()
    {
        if (finished) return;
        anim = true;
        if (firstTime)
        {
            firstTime = false;
            timer_trans = 0;
        }
    }
    public void End()
    {
        if (finished) return;
        anim = false;
    }

    private void Update()
    {
        if (finished) return;
        
        if (!anim) return;

        if (timer_trans < time_to_trans)
        {
            timer_trans = timer_trans + 1 * Time.deltaTime;

            float lerpValue = curve.Evaluate(timer_trans / time_to_trans);

            for (int i = 0; i < anims_lerped.Length; i++)
            {
                anims_lerped[i].OnLerp(lerpValue);
            }
        }
        else
        {
            finished = true;
            timer_trans = 0;
            anim = false;
            onFinish.Invoke();
        }
        
    }
}
