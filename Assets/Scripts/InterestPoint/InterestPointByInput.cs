using TMPro;
using UnityEngine;

public class InterestPointByInput : InterestPoint
{
    bool canInput;
    bool spended = false;

    [SerializeField] int interestQuantity = 20;

    [SerializeField] Transform pointToScore;

    protected override void OnEnter()
    {
        if (spended) return;
        canInput = true;
        PointOfInterestSystem.Instance.ShowJumpBar();
    }

    protected override void OnExit()
    {
        canInput = false;
        PointOfInterestSystem.Instance.HideJumpBar();
    }

    protected override void OnFinishVisit()
    {
        spended = true;
        SoundFX.PlaySound("camera");
        FLash.Instance.SnapShot();
        ScoreFeedbackManager.ShowScoreInPos("+"+interestQuantity, Color.green, pointToScore.position);
        PointOfInterestSystem.Instance.AddInterest(interestQuantity);
        PointOfInterestSystem.Instance.HideJumpBar();

        InfoMonument.Instance.SetInfoFastPhoto(myMonument.nameOfMonument, myMonument.image);
    }

    private void Update()
    {
        if (spended) return;

        if (canInput)
        {
            if (Input.GetButtonDown("Jump"))
            {
                FinishVisit();
            }
        }
    }
}
