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
    bool OnExited=false;

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
    
        progresor.Begin();
    }

    protected override void OnExit()
    {
        if (canReactive == false) return;
        imInAMonument = false;
        OnExited = true;
        SOManager.instance.myImage.SetActive(false);
        progresor.End();
    }

    private void Update()
    {
        if(visited) return;
        if (imInAMonument && OnExited==false && Player.instance.speed <= 0.5f)
        {
            CameraTarget.instance.isFollowing = false;
            cam.transform.position = Vector3.Lerp(cam.transform.position, myTransformOfCam.transform.position, moveSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, myTransformOfCam.transform.rotation, moveSpeed * Time.deltaTime);
            if(Vector3.Distance(cam.transform.position, myTransformOfCam.transform.position) < 0.1f)
            {
                posBeforeEnter = cam.transform.position;
            }
        }

        if (OnExited && imInAMonument==false)
        {
            /*cam.transform.position = Vector3.Lerp(cam.transform.position, posBeforeEnter, returnSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime);*/

           
                CameraTarget.instance.isFollowing = true;
                OnExited = false;

            
        }
    }
    protected override void OnFinishVisit()
    {
        Debug.Log("Interest Point Visited: " + gameObject.name);
        PointOfInterestSystem.Instance.CompleteInterest();
        visited = true;
    }
}