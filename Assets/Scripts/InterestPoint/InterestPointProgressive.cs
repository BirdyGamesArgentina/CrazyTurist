using UnityEngine;

public class InterestPointProgressive : InterestPoint
{
    //[SerializeField] ProgressionModule progresor;
   
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float returnSpeed = 3.5f;
    [SerializeField] float cameraHeight = 7f;
    bool canReactive = true;
    bool OnExited = false;

    bool visited = false, imInAMonument = false;
    Vector3 originalPosition;
    public GameObject myTransformOfCam;
    public Transform[] peopleTransform;

    Transform cam;

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
        //progresor.SetCallbackOnFinish(FinishVisit);

        cam = Camera.main.transform;
    }

    float timer_cam;
    [SerializeField] float time_ToMove = 1f;

    Vector3 firstPosition;
    Quaternion firstRotation;

    protected override void OnEnter()
    {
        if (visited) return;

        CameraTarget.instance.isFollowing = false;
        Player.instance.CanMove(false);
        imInAMonument = true;
        timer_cam = 0;
        firstPosition = Camera.main.transform.position;
        firstRotation = Camera.main.transform.rotation;

        if (canReactive == false) return;
        SoundFX.PlaySound("GenteFestejando");
        //progresor.Begin();
    }

    protected override void OnExit()
    {

        SoundFX.StopSound("GenteFestejando");

        if (canReactive == false) return;
        imInAMonument = false;
        OnExited = true;

        if (visited)
            GetComponent<Collider>().enabled = false;

    }

    bool cd_to_back = false;
    [SerializeField] float time_to_show = 3f;
    float timer;

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


        if (imInAMonument)
        {
            if (timer_cam < time_ToMove)
            {
                timer_cam += Time.deltaTime;

                cam.position = Vector3.Lerp(firstPosition, myTransformOfCam.transform.position, timer_cam / time_ToMove);
                cam.rotation = Quaternion.Lerp(firstRotation, myTransformOfCam.transform.rotation, timer_cam / time_ToMove);
            }
            else
            {
                imInAMonument = false;
                timer_cam = 0;
                GenericBar.Show(true);
                cd_to_back = true;
                timer = 0;
            }
        }

        //if (OnExited && imInAMonument == false)
        //{
        //    CameraTarget.instance.isFollowing = true;
        //    OnExited = false;
        //}

        if (cd_to_back)
        {
            if (timer < time_to_show)
            {
                timer += Time.deltaTime;
                GenericBar.SetValue(timer/time_to_show);
            }
            else
            {
                timer = 0;
                cd_to_back = false;
                GenericBar.Show(false);
                FinishVisit();
            }
        }
    }


    protected override void OnFinishVisit()
    {
        Player.instance.CanMove(true);
        imInAMonument = false;
        CameraTarget.instance.isFollowing = true;
        SoundFX.PlaySound("camera_monument");
        FLash.Instance.SnapShot();
        Debug.Log("Interest Point Visited: " + gameObject.name);
        PointOfInterestSystem.Instance.CompleteInterest();
        InfoMonument.Instance.SetInfoBigMonument(myMonument.nameOfMonument, myMonument.informationText, myMonument.image);
        visited = true;
    }
}