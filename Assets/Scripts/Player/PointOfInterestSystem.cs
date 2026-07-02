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
        Debug.Log("Enter: " + _ip.gameObject.name);
        _ip.OnEnter();
    }
    public void OnExitInterestPoint(InterestPoint _ip)
    {
        Debug.Log("Exit: " + _ip.gameObject.name);

        _ip.OnExit();
    }
}
