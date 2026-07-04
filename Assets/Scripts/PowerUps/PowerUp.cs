using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private PowerUpSpawner _spawner;
    [SerializeField] float powerUpTime = 2;

    [SerializeField] string timerKey = "";
    [SerializeField] Collider col;
    protected Player _player;


    public void Spawn(PowerUpSpawner spawner)
    {
        _spawner = spawner;
    }

    public void PickUp(Player player)
    {
        OnPickUp(player);
        if (TimerManager.Instance.TimerExist(timerKey))
        {
            TimerManager.Instance.StopTimer(timerKey, true);
        }
        _player = player;
        StartPowerUp(player);
        TimerManager.Instance.AddTimer(powerUpTime, EndPowerUp, timerKey);
        _spawner.DeletePowerUp(this);
    }

    protected abstract void OnPickUp(Player player);
    private void StartPowerUp(Player player)
    {
        col.enabled = false;
        col.transform.parent = player.transform;
        OnStartPowerUp(player);
    }

    private void EndPowerUp()
    {
        OnEndPowerUp();
        Destroy(this.gameObject);
    }

    protected abstract void OnStartPowerUp(Player player);
    protected abstract void OnEndPowerUp();
}