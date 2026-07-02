using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target;
    [SerializeField] float lerpAmount = 0.1f;

    Vector3 offset;
    private void Start()
    {
        offset = target.position - transform.position;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, target.position - offset, lerpAmount);
    }
}
