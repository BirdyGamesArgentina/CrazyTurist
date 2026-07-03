using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 18f;
    [SerializeField] private float deceleration = 25f;
    [SerializeField] private float rotationSpeed = 15f;

    private Rigidbody rb;

    private Vector3 input;
    private Vector3 currentVelocity;
    public static Player instance;
    [Header("Contain")]
    public Transform[] Persons;
    public bool[] PositionUsed;
    public int GetPersonAmount() => Persons.Count(x => x.childCount != 0);


    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();

        PositionUsed = new bool[Persons.Length];
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        input.Normalize();
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = input * moveSpeed;

        float accelRate = input.sqrMagnitude > 0.01f ? acceleration : deceleration;

        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, accelRate * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(currentVelocity.x, rb.linearVelocity.y, currentVelocity.z);

        if (input != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        }
    }

}
