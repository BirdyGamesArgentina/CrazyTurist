using UnityEngine;
using UnityEngine.UI;

public abstract class PowerUp : MonoBehaviour
{
    [Header("Power UP")]
    [SerializeField] float powerUpTime = 2;
    private PowerUpSpawner _spawner;

    [SerializeField] string timerKey = "";
    [SerializeField] Collider col;
    protected Player _player;

    [Header("Power UP Data")]
    [SerializeField] Sprite _sprite;
    [SerializeField] string _description;

    [SerializeField] GameObject[] tohideonCollect;


    public void Spawn(PowerUpSpawner spawner)
    {
        _spawner = spawner;
    }

    public void PickUp(Player player)
    {
        OnPickUp(player);
        
        //PowerUpsUI.SetValues();

        if (TimerManager.Instance.TimerExist(timerKey))
        {
            TimerManager.Instance.StopTimer(timerKey, true);
        }
        _player = player;
        StartPowerUp(player);
        TimerManager.Instance.AddTimerLerp(powerUpTime, EndPowerUp, timerKey, Anim_CallbackLerp);
        _spawner?.DeletePowerUp(this);
    }

    void Anim_CallbackLerp(float _lerp)
    {
        PowerUpsUI.UpdateValue(1 - _lerp/ powerUpTime);
    }

    protected abstract void OnPickUp(Player player);
    private void StartPowerUp(Player player)
    {
        PowerUpsUI.SetValues(_description, _sprite);


        for (int i = 0; i < tohideonCollect.Length; i++)
        {
            tohideonCollect[i].SetActive(false);
        }
        

        OnStartPowerUp(player);
    }

    private void EndPowerUp()
    {
        PowerUpsUI.Hide();
        OnEndPowerUp();
        Destroy(this.gameObject);
    }

    protected abstract void OnStartPowerUp(Player player);
    protected abstract void OnEndPowerUp();

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}