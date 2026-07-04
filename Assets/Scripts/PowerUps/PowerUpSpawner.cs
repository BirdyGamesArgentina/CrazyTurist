using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] PowerUp[] possiblesPowerUps;
    [SerializeField] float maxTimeSpawn;
    [SerializeField] float minTimeSpawn;


    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        var selected = possiblesPowerUps[Random.Range(0, possiblesPowerUps.Length)];
        var current = Instantiate(selected, transform);
        current.Spawn(this);
    }

    public void DeletePowerUp(PowerUp powerUp)
    {
        TimerManager.Instance.AddTimer(Random.Range(minTimeSpawn, maxTimeSpawn), Spawn);
    }
}
