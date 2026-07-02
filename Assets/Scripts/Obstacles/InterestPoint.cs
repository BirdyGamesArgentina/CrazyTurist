using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InterestPoint : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    public TextMeshProUGUI deb;

    /// <summary>
    /// LerpedObject tienen una funcion de OnLerp para que se le puedan ejecutar diferentes animaciones
    /// que vayan en paralelo con la curva de animacion
    /// </summary>
    [SerializeField] LerpedObject[] anims_lerped; // ejecuta el OnLerp(float)

    [SerializeField] float time_to_trans = 1f;
    float timer_trans = 1f;
    bool anim = false;
    [SerializeField] AnimationCurve curve;

    public void OnEnter()
    {
        deb.text = "ENTER";
        onEnter.Invoke();
        anim = true;
        timer_trans = 0f;
    }

    public void OnExit()
    {
        deb.text = "EXIT";
        onExit.Invoke();
        anim = false;
        timer_trans = 0f;
    }

    private void Update()
    {
        if (anim)
        {
            if (timer_trans < time_to_trans)
            {
                timer_trans = timer_trans + 1 * Time.deltaTime;

                float lerpValue = curve.Evaluate(timer_trans/ time_to_trans);

                for (int i = 0; i < anims_lerped.Length; i++)
                {
                    anims_lerped[i].OnLerp(lerpValue);
                }
            }
            else
            {
                timer_trans = 0;
                anim = false;
            }
        }
    }
}
