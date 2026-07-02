using UnityEngine;

public class ObstacleReceiver : MonoBehaviour
{
    public static ObstacleReceiver Instance;

    [SerializeField] Sensor sensor;
    [SerializeField] ParticleSystem particleSparks;
    public static string PARTICLE_SPARKS_NAME = "particle_sparks";

    [SerializeField] Transform player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        sensor.SubscribeToObstacle(OnHitObstacle);

        ParticlePool.AddRegistry(PARTICLE_SPARKS_NAME, particleSparks);
    }

    void OnHitObstacle(Obstacle obstacle)
    {
        Debug.Log("Obstacle: " + obstacle.gameObject.name);

        Vector3 dir = obstacle.transform.position - player.position;
        
        var p = ParticlePool.Get(PARTICLE_SPARKS_NAME, true);
        p.transform.position = player.position  + dir / 2f;
        p.Play();

        dir.Normalize();
        obstacle.HitKnockback(dir);
    }
}
