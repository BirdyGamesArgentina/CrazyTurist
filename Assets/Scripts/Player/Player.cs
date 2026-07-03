using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Contain")]
    public Transform[] Persons;
    public bool[] PositionUsed;
    public static int PeopleAmount { get { return instance.Persons.Count(x => x.childCount != 0); } }

    [Header("Game Feel")]
    public GameObject[] wheels;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints =
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ;
        PositionUsed = new bool[Persons.Length];
    }

    [Header("Speed")]
    public float maxSpeed = 30f;
    public float reverseSpeed = 10f;
    public float acceleration = 20f;
    public float brake = 40f;
    public float coast = 10f;

    [Header("Steering")]
    public float steerAngle = 140f;
    public AnimationCurve steeringBySpeed =
        AnimationCurve.Linear(0, 1, 1, 0.35f);
    public float rotationFacility = 300f;

    [Header("Grip")]
    [Range(0, 1)]
    public float grip = 8f;

    Rigidbody rb;

    float speed;
    private float currentSteerVelocity;

    void FixedUpdate()
    {
        float throttle = Input.GetAxisRaw("Vertical");
        float steer = Input.GetAxisRaw("Horizontal");

        float wheelAngle = steer * 35f;

        Quaternion targetRotation = Quaternion.Euler(0, wheelAngle, 0);

        wheels[0].transform.localRotation = Quaternion.Slerp(wheels[0].transform.localRotation, targetRotation, 8f * Time.fixedDeltaTime);

        wheels[1].transform.localRotation = Quaternion.Slerp(wheels[1].transform.localRotation, targetRotation, 8f * Time.fixedDeltaTime);




        float steerForBus = steer;

        if (Mathf.Abs(speed) < 0.1f)
            steerForBus = 0f;

        //
        // SPEED
        //
        if (throttle > 0)
        {
            speed += acceleration * Time.fixedDeltaTime;
        }
        else if (throttle < 0)
        {
            if (speed > 0)
            {
                speed -= brake * Time.fixedDeltaTime;

  
            }
            else
                speed -= acceleration * Time.fixedDeltaTime;
        }
        else
        {
            speed = Mathf.MoveTowards(
                speed,
                0,
                coast * Time.fixedDeltaTime);
        }

        speed = Mathf.Clamp(speed, -reverseSpeed, maxSpeed);

        //
        // STEERING
        //
        float steeringMultiplier =
            steeringBySpeed.Evaluate(Mathf.Abs(speed) / maxSpeed);

        float yaw =
            steer *
            steerAngle *
            steeringMultiplier *
            Time.fixedDeltaTime;

        float speed01 = Mathf.Clamp01(Mathf.Abs(speed) / maxSpeed);

        float maxYawSpeed = Mathf.Lerp(180f, 70f, speed01);

        float targetYawSpeed = steerForBus * maxYawSpeed;

        currentSteerVelocity = Mathf.MoveTowards(
            currentSteerVelocity,
            targetYawSpeed,
            rotationFacility * Time.fixedDeltaTime); // rapidez con la que "agarra" el volante

        transform.Rotate(0f, currentSteerVelocity * Time.fixedDeltaTime, 0f);

        //
        // VELOCITY
        //
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);

        localVelocity.z = speed;

        localVelocity.x = Mathf.Lerp(
            localVelocity.x,
            0,
            grip * Time.fixedDeltaTime);


        Vector3 desiredVelocity = transform.forward * speed;

        // Conservá la componente vertical para que la gravedad siga funcionando.
        desiredVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = Vector3.Lerp(
            rb.linearVelocity,
            desiredVelocity,
            10f * Time.fixedDeltaTime);
    }


}
