using TMPro;
using UnityEngine;

public enum Side
{
    left,
    right
}

public class InterestPointByInput : InterestPoint
{

    public Side side;
    bool canInput;
    bool spended = false;

    [SerializeField] int interestQuantity = 20;

    [SerializeField] Transform pointToScore;

    protected override void OnEnter()
    {
        if (spended) return;
        canInput = true;
        PointOfInterestSystem.Instance.ShowSideFeedback(side);
    }

    protected override void OnExit()
    {
        canInput = false;
        PointOfInterestSystem.Instance.HideSideFeedback();
    }

    protected override void OnFinishVisit()
    {
        spended = true;
        SoundFX.PlaySound("camera");
        FLash.Instance.SnapShot();
        ScoreFeedbackManager.ShowScoreInPos("+"+interestQuantity, Color.green, pointToScore.position);
        PointOfInterestSystem.Instance.AddInterest(interestQuantity);
        PointOfInterestSystem.Instance.HideSideFeedback();
    }

    private void Update()
    {
        if (spended) return;

        if (canInput)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && side == Side.left)
            {
                FinishVisit();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && side == Side.right)
            {
                FinishVisit();
            }
        }
    }
}
