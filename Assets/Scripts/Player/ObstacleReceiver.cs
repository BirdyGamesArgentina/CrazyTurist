using UnityEngine;

public class ObstacleReceiver : MonoBehaviour
{
    public static ObstacleReceiver Instance;

    [SerializeField] Sensor sensor;
    [SerializeField] ParticleSystem particleSparks;
    [SerializeField] ParticleSystem explosionParticle;
    public static string PARTICLE_SPARKS_NAME = "particle_sparks";

    [SerializeField] Transform player;
    bool invulnerable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        sensor.SubscribeToObstacle(OnHitObstacle);

        ParticlePool.AddRegistry(PARTICLE_SPARKS_NAME, particleSparks);
        ParticlePool.AddRegistry(explosionParticle.name, explosionParticle);
    }

    public void SetInvulnerable(bool _invulnerable)
    {
        invulnerable = _invulnerable;
        sensor.transform.localScale = invulnerable ? Vector3.one *1.5f : Vector3.one;
    }

    void OnHitObstacle(Obstacle obstacle)
    {
        Debug.Log("Obstacle: " + obstacle.gameObject.name);

        Vector3 dir = obstacle.transform.position - player.position;

        var p = ParticlePool.Get(PARTICLE_SPARKS_NAME, true);
        var collision_center = player.position + dir / 2f;

        p.transform.position = collision_center;
        p.Play();

        if (invulnerable)
        {
            var explosion = ParticlePool.Get(PARTICLE_SPARKS_NAME, true);

            explosion.transform.position = obstacle.transform.position;
            explosion.Play();
            obstacle.Explode();
            return;
        }

        PointOfInterestSystem.Instance.RemoveInterest(obstacle.InterestToRemove);
        ScoreFeedbackManager.ShowScoreInPos("-" + obstacle.InterestToRemove, Color.red, collision_center + Vector3.up);

        dir.Normalize();
        obstacle.HitKnockback(dir);
    }
}
