using Mono.Cecil;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;

public class InterestPointProgressive : InterestPoint
{
    [SerializeField] ProgressionModule progresor;
    public GameObject cam;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float returnSpeed =3.5f; 
    [SerializeField] float cameraHeight = 7f;
    bool canReactive = true;

    bool visited = false , imInAMonument=false;   
    Vector3 originalPosition;
    public GameObject myTransformOfCam;


    private void Start()
    {
        if (canReactive == false) return;
        progresor.SetCallbackOnFinish(FinishVisit);
    }
    Vector3 posBeforeEnter; 
    Vector3 currentPos;

    protected override void OnEnter()
    {
        if (canReactive == false) return;
        imInAMonument = true;
        CameraTarget.instance.isFollowing = false;
        progresor.Begin();
    }

    protected override void OnExit()
    {
        if (canReactive == false) return;
        imInAMonument = false;
        visited = true;
        SOManager.instance.myImage.SetActive(false);
        progresor.End();
    }

    private void Update()
    {

        if (canReactive == false) return;
        if (imInAMonument)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, myTransformOfCam.transform.position, moveSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, myTransformOfCam.transform.rotation, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(cam.transform.position, myTransformOfCam.transform.position) < 0.1f)
            {
                posBeforeEnter = cam.transform.position;
            }
        }

        if (visited)
        {
            /*cam.transform.position = Vector3.Lerp(cam.transform.position, posBeforeEnter, returnSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime);*/

            if (Vector3.Distance(cam.transform.position, posBeforeEnter) < 0.5f&& visited==true)
            {
                visited = false;
                CameraTarget.instance.isFollowing = true;
                canReactive = false;

            }
        }
    }
    protected override void OnFinishVisit()
    {
        PointOfInterestSystem.Instance.CompleteInterest();
    }
}