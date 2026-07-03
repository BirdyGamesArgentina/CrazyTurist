using UnityEngine;
using UnityEngine.UI;

public class PointOfInterestSystem : MonoBehaviour
{
    public static PointOfInterestSystem Instance;

    [SerializeField] Sensor sensor;
    [SerializeField] private Image img;
    [SerializeField] private float duration = 10f;
    public float interest = 100;
    [SerializeField] int maxInterest = 100;

    [SerializeField] float quantToRemove = 2f;

    [SerializeField] GameObject leftSide;
    [SerializeField] GameObject rightSide;
    [SerializeField] Transform player;


    bool anim = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        sensor.SubscribeToInterestPoint(OnEnterInterestPoint, OnExitInterestPoint);
        img.fillAmount = 1f;
    }

    public void OnEnterInterestPoint(InterestPoint _ip)
    {
        _ip.Enter();
    }
    public void OnExitInterestPoint(InterestPoint _ip)
    {
        _ip.Exit();
    }

    
    public void BeginCountDown()
    {
        anim = true;
        interest = maxInterest;

    }

    private void Update()
    {
        if (!anim) return;
        interest = interest - quantToRemove * Time.deltaTime;
        Refresh();

        leftSide.transform.position = player.transform.position + Vector3.up + Vector3.left;
        rightSide.transform.position = player.transform.position + Vector3.up + Vector3.right;
    }

    void Refresh()
    {
        img.fillAmount = (float)interest / maxInterest;
    }

    public void AddInterest(int toAdd)
    {
        interest += toAdd;
        Refresh();
    }

    public void RemoveInterest(int toRemove)
    {
        interest -= toRemove;
        Refresh();
    }

    public void ShowSideFeedback(Side side)
    {
        if (side == Side.left)
        {
            leftSide.SetActive(true);
        }
        else if(side == Side.right)
        {
            rightSide.SetActive(true);
        }
    }
    public void HideSideFeedback()
    {
        leftSide.SetActive(false);
        rightSide.SetActive(false);
    }
}
