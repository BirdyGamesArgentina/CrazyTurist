using UnityEngine;

public class PathFindingOptions : MonoBehaviour
{
    public static PathFindingOptions instance;

    public float detectionRadius = 2f;
    public LayerMask layerNode;
    public float closeDist = 0.5f;
    public float speed = 5f;
    public float steeringForce = 0.1f;

    [Header("Theta Options")]
    public LayerMask thetaObstacle; // obstacles, Walls
    public float sphereCastRadius = 0.2f;

    [SerializeField] Transform[] positions;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Vector3 GetRandomPosition()
    {
        return instance.positions[Random.Range(0, instance.positions.Length)].position;
    }
}
