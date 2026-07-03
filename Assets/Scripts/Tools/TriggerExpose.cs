using UnityEngine;
using UnityEngine.Events;

public class TriggerExpose : MonoBehaviour
{
    public bool player;

    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                OnEnter.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                OnExit.Invoke();
            }
        }
    }
}
