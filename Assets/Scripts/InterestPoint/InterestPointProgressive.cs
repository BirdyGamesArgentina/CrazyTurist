using TMPro;
using UnityEngine;

public class InterestPointProgressive : InterestPoint
{

    [SerializeField] ProgressionModule progresor;

    private void Start()
    {
        progresor.SetCallbackOnFinish(FinishVisit);
    }

    protected override void OnEnter()
    {
        if (myMonument == null)
        {
            SOManager.instance.myNameOnCanva.text = myMonument.nameOfMonument;
            SOManager.instance.MyInformationInCanva.text = myMonument.informationText;
            SOManager.instance.myImage.SetActive(true);
        }
        progresor.Begin();

    }
    protected override void OnExit()
    {
        SOManager.instance.myImage.SetActive(false);

        progresor.End();
    }

    protected override void OnFinishVisit()
    {
        PointOfInterestSystem.Instance.CompleteInterest();
    }
}
