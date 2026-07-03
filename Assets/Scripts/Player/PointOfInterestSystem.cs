using Game.Scripts.Shared.Events;
using Game.Scripts.Shared.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

public class PointOfInterestSystem : MonoBehaviour
{
    public static PointOfInterestSystem Instance;


    float interest = 100;
    [SerializeField] int maxInterest = 100;

    public static float Interest { get { return Instance.interest; } }
    public static float MaxInterest { get { return Instance.maxInterest; } }


    [SerializeField] Sensor sensor;
    [SerializeField] private Image img;
    [SerializeField] private float duration = 10f;
   

    [SerializeField] float quantToRemove = 2f;

    [SerializeField] GameObject leftSide;
    [SerializeField] GameObject rightSide;
    [SerializeField] Transform player;

    [SerializeField] private float scoreMultiplier;
    private long _currentScore;
    private InterestPoint lastInterestPoint;


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
        if(_ip != lastInterestPoint)
        {
            lastInterestPoint = _ip;
            _currentScore += (long)(player.GetComponent<Player>().GetPersonAmount() * scoreMultiplier * (interest / maxInterest));
        }
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
        RemoveInterest(quantToRemove * Time.deltaTime);

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

    public void RemoveInterest(float toRemove)
    {
        interest -= toRemove;
        Refresh();

        if(interest <= 0)
        {
            ServiceLocator.Instance.GetService<IEventBus>().Publish(new OnEndScreenResult("Perdiste", _currentScore));
        }
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
