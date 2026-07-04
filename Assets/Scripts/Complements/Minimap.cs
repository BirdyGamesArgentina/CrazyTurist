using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Minimap : MonoBehaviour
{
    public Transform target;
    public float lerpAmount;
    
    void LateUpdate()
    {
            transform.position = Vector3.Slerp(transform.position, new Vector3(target.position.x,transform.position.y,target.position.z), lerpAmount);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(90, 0, 0), Quaternion.identity, 0);
    }
}
