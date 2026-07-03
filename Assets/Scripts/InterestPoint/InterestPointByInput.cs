using TMPro;
using UnityEngine;

public class InterestPointByInput : InterestPoint
{
    [SerializeField] TextMeshPro _debug;

    bool canInput;

    protected override void OnEnter()
    {
        canInput = true;
    }

    protected override void OnExit()
    {
        canInput = false;
    }

    protected override void OnFinishVisit()
    {
        
    }

    private void Update()
    {
        if (canInput)
        {
            _debug.text = "Left: " + Input.GetKey(KeyCode.LeftArrow) + " Right: " + Input.GetKey(KeyCode.RightArrow);
        }
    }
}
