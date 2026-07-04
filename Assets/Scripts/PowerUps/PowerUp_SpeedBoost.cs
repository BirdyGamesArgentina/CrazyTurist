using UnityEngine;

public class PowerUp_SpeedBoost : PowerUp
{
    [SerializeField] float maxSpeedToAdd = 5;
    [SerializeField] float accelerationToAdd = 2;

    protected override void OnEndPowerUp()
    {
        _player.SpeedUp(-maxSpeedToAdd, -accelerationToAdd);
    }

    protected override void OnPickUp(Player player)
    {
    }

    protected override void OnStartPowerUp(Player player)
    {
        player.SpeedUp(maxSpeedToAdd, accelerationToAdd);
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        base.OnDrawGizmos();
    }
}
