using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target;
    [SerializeField] float lerpAmount = 0.1f;
    public bool isFollowing = true;
    Vector3 offset;

    public Vector3 transformer;
    public  Quaternion rotationTransformer;


    public static CameraTarget instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        offset = target.position - transform.position;
    }

    public Vector3 GetFollowPosition()
    {
        return target.position - offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Slerp(transform.position, target.position - offset, lerpAmount);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(67, 0, 0), Quaternion.identity, 0);

        }
  
    }
}
