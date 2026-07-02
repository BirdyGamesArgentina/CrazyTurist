using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target, camera;



  
    // Update is called once per frame
    void Update()
{
        Vector3 cameraTransform = new Vector3(target.position.x, camera.position.y, target.position.z);

       camera.position = cameraTransform;
    }
}
