using UnityEngine;

public class PointOfInterestSystem : MonoBehaviour
{
    [SerializeField] Sensor sensor;

    private void Start()
    {
        sensor.SubscribeToInterestPoint(OnEnterInterestPoint, OnExitInterestPoint);
    }

    public void OnEnterInterestPoint(InterestPoint _ip)
    {
        _ip.Enter();
    }
    public void OnExitInterestPoint(InterestPoint _ip)
    {
        _ip.Exit();
    }
}
