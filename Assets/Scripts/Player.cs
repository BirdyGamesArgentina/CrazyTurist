using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
  
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 18f;
    [SerializeField] private float deceleration = 25f;

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input = input.normalized;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = input * moveSpeed;

        float accelRate = input.sqrMagnitude > 0.01f ? acceleration : deceleration;

        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, accelRate * Time.fixedDeltaTime);

        rb.linearVelocity = currentVelocity;

        if (input != Vector2.zero)
        {
            float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            rb.MoveRotation(angle - 90f); 
        }
    }



}
