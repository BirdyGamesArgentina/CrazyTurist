using UnityEngine;
using UnityEngine.UI;

public class Bar_2D : LerpedObject
{
    [SerializeField] Image fillbar;

    public override void OnLerp(float _lerpValue)
    {
        fillbar.fillAmount = _lerpValue;
    }
}
