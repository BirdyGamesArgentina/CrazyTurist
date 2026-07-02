using UnityEngine;

public class ObstacleReceiver : MonoBehaviour
{
    [SerializeField] Sensor sensor;

    private void Awake()
    {
        sensor.SubscribeToObstacle(OnHitObstacle);
    }

    void OnHitObstacle(Obstacle obstacle)
    {

    }
}
