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

    protected override void OnEnter()
    {
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
        PointOfInterestSystem.Instance.AddInterest(interestQuantity);
    }

    private void Update()
    {
        if (spended) return;

        if (canInput)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && side == Side.left)
            {
                spended = true;
                FinishVisit();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && side == Side.right)
            {
                spended = true;
                FinishVisit();
            }
        }
    }
}
