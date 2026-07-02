using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] float obstacleSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = transform.position + direction * Time.deltaTime;
    }
}
