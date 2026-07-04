using UnityEngine;

public class PowerUp_InterestLock : PowerUp
{
    [SerializeField] Light prefab;
    [SerializeField] float changeFeedbackTime = 0.5f;

    private Light current;
    private float timer;

    protected override void OnEndPowerUp()
    {
        PointOfInterestSystem.Instance.InterestLock(false);
    }

    protected override void OnPickUp(Player player)
    {
    }

    protected override void OnStartPowerUp(Player player)
    {
        PointOfInterestSystem.Instance.InterestLock(true);
        current = Instantiate(prefab, transform);
        transform.localPosition = new Vector3(0, 0.8f, -1.25f);
    }

    private void Update()
    {
        if (current != null)
        {
            timer += Time.deltaTime;
            if(timer > changeFeedbackTime)
            {
                current.color = new Color(Random.value, Random.value, Random.value);
                timer = 0;
            }
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        base.OnDrawGizmos();
    }
}
