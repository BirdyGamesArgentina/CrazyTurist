using UnityEngine;
using UnityEngine.UI;

public class FLash : MonoSingleton<FLash>
{
    [SerializeField] float time_to_flash = 0.1f;
    float timer;
    bool anim = false;
    [SerializeField] Image img;
    [SerializeField] AnimationCurve curveFade;

    Color white = new Color(1, 1, 1, 1);
    Color trans = new Color(1, 1, 1, 0);

    protected override void OnAwake()
    {
        
    }

    public void SnapShot()
    {
        anim = true;
        timer = 0;
    }

    void Update()
    {
        if (anim)
        {
            if (timer < time_to_flash)
            {
                timer = timer + 1 * Time.deltaTime;

                img.color = Color.Lerp(white, trans, curveFade.Evaluate(timer/time_to_flash));
            }
            else
            {
                timer = 0;
                anim = false;
                img.color = trans;
            }
        }
    }
}
