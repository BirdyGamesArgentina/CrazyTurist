using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public Action<Obstacle> OnHitObstacle;
    public Action<InterestPoint> OnEnterInterestPoint;
    public Action<InterestPoint> OnExitInterestPoint;


    public void SubscribeToObstacle(Action<Obstacle> _onHitObstacle)
    {
        OnHitObstacle = _onHitObstacle;
    }
    public void SubscribeToInterestPoint(Action<InterestPoint> _onEnterIP, Action<InterestPoint> _onExitIP)
    {
        OnEnterInterestPoint = _onEnterIP;
        OnExitInterestPoint = _onExitIP;
    }

    public void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            OnHitObstacle.Invoke(obstacle);
            SoundFX.PlaySound("AutoChocando");
        }

        InterestPoint interest = other.GetComponent<InterestPoint>();
        if (interest != null)
        {
            OnEnterInterestPoint.Invoke(interest);
        }
        PowerUp powerUp = other.GetComponent<PowerUp>();
        if (powerUp != null)
        {
            powerUp.PickUp(Player.instance);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InterestPoint interest = other.GetComponent<InterestPoint>();
        if (interest != null)
        {
            OnExitInterestPoint.Invoke(interest);
        }
    }

}
