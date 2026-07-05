using Mono.Cecil;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;

public class InterestPointProgressive : InterestPoint
{
    [SerializeField] ProgressionModule progresor;
    public GameObject cam;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float returnSpeed = 3.5f;
    [SerializeField] float cameraHeight = 7f;
    bool canReactive = true;
    bool OnExited = false;

    bool visited = false, imInAMonument = false;
    Vector3 originalPosition;
    public GameObject myTransformOfCam;
    public Transform[] peopleTransform;



    [SerializeField] float peopleSpeed = 2f;

    private Transform[] movingPeople;
    private Transform[] targetPoints;
    private bool peopleWalking;
    [SerializeField] private GameObject[] monumentPeople;

    public static InterestPointProgressive instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (canReactive == false) return;
        progresor.SetCallbackOnFinish(FinishVisit);
    }
    Vector3 posBeforeEnter;
    Vector3 currentPos;

    protected override void OnEnter()
    {
        if (visited) return;


        if (canReactive == false) return;

        SoundFX.PlaySound("GenteFestejando");

        imInAMonument = true;

        progresor.Begin();
    }

    protected override void OnExit()
    {

        SoundFX.StopSound("GenteFestejando");

        if (canReactive == false) return;
        imInAMonument = false;
        OnExited = true;
        //SOManager.instance.myImage.SetActive(false);
        progresor.End();

        if (visited)
            GetComponent<Collider>().enabled = false;

    }

    private void Update()
    {

        if (peopleWalking)
        {
            bool allArrived = true;

            for (int i = 0; i < movingPeople.Length; i++)
            {
                if (movingPeople[i] == null)
                    continue;

                movingPeople[i].position = Vector3.MoveTowards(movingPeople[i].position, targetPoints[i].position, peopleSpeed * Time.deltaTime);

                movingPeople[i].rotation = Quaternion.RotateTowards(movingPeople[i].rotation, targetPoints[i].rotation, 360f * Time.deltaTime);

                if (Vector3.Distance(movingPeople[i].position, targetPoints[i].position) > 0.05f)
                    allArrived = false;
            }

            if (allArrived)
                peopleWalking = false;
        }


        if (imInAMonument && OnExited == false && Player.instance.speed <= 0.5f)
        {
            CameraTarget.instance.isFollowing = false;
            cam.transform.position = Vector3.Lerp(cam.transform.position, myTransformOfCam.transform.position, moveSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, myTransformOfCam.transform.rotation, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(cam.transform.position, myTransformOfCam.transform.position) < 0.1f)
            {
                posBeforeEnter = cam.transform.position;
            }
        }

        if (OnExited && imInAMonument == false)
        {
            /*cam.transform.position = Vector3.Lerp(cam.transform.position, posBeforeEnter, returnSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime);*/


            CameraTarget.instance.isFollowing = true;
            OnExited = false;


        }
    }
    protected override void OnFinishVisit()
    {
        SoundFX.PlaySound("camera_monument");
        FLash.Instance.SnapShot();
        Debug.Log("Interest Point Visited: " + gameObject.name);
        PointOfInterestSystem.Instance.CompleteInterest();
        InfoMonument.Instance.SetInfoBigMonument(myMonument.nameOfMonument, myMonument.informationText, myMonument.image);
        visited = true;
    }
}