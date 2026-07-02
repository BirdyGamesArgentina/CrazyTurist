using UnityEngine;

public class Bar_3D : LerpedObject
{
    [SerializeField] Transform fillBar3D;

    [SerializeField] bool x;
    [SerializeField] bool y;
    [SerializeField] bool z;

    private void Start()
    {
        original = fillBar3D.transform.localScale;
        current = original;
    }

    Vector3 original = Vector3.zero;
    Vector3 current = Vector3.zero;
    public override void OnLerp(float _lerpValue)
    {
        if(x) current.x = _lerpValue;
        if(y) current.y = _lerpValue;
        if(z) current.z = _lerpValue;

        fillBar3D.localScale = current;
    }
}
