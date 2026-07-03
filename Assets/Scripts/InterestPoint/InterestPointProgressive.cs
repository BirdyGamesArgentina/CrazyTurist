using TMPro;
using UnityEngine;

public class InterestPointProgressive : InterestPoint
{

    [Header("Tiempo de Completado")]
    [SerializeField] float time_to_trans = 1f;
    float timer_trans = 1f;
    bool anim = false;
    bool oneshotAnim = false;
    [SerializeField] AnimationCurve curve;
    [SerializeField] LerpedObject[] anims_lerped; // ejecuta el OnLerp(float)
    protected override void OnEnter()
    {
        SOManager.instance.myNameOnCanva.text = myMonument.nameOfMonument;
        SOManager.instance.MyInformationInCanva.text = myMonument.informationText;
        SOManager.instance.myImage.SetActive(true);
        anim = true;
        timer_trans = 0f;

    }
    protected override void OnExit()
    {
        SOManager.instance.myImage.SetActive(false);
        anim = false;
        timer_trans = 0f;
    }

    protected override void OnFinishVisit()
    {
        
    }

    

    private void Update()
    {
        if (!oneshotAnim)
        {
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
                oneshotAnim = true;
                timer_trans = 0;
                anim = false;
                FinishVisit();
            }
        }
    }
}
