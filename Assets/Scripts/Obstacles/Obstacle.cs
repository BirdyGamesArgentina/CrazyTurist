using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] float obstacleSpeed;
    [SerializeField] Rigidbody myRb;

    [SerializeField] bool applyKnockBack;
    [SerializeField] float knockbackForce = 5f;

    void Update()
    {
        transform.forward = direction;
    }

    private void FixedUpdate()
    {
        myRb.linearVelocity = direction * Time.deltaTime * obstacleSpeed;
    }

    public void HitKnockback(Vector3 dir)
    {
        myRb.AddForce(dir * knockbackForce, ForceMode.VelocityChange);
    }

}
