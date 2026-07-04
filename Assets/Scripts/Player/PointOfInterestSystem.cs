using Game.Scripts.Shared.Events;
using Game.Scripts.Shared.ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointOfInterestSystem : MonoBehaviour
{
    public static PointOfInterestSystem Instance;


    float interest = 100;
    [SerializeField] int maxInterest = 100;

    [Range(0f,1f)]
    [SerializeField] float percentInterestToWin;

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
    private InterestPoint lastInterestPoint;

    private long _currentScore;
    public static long CurrentScore => Instance._currentScore;

    [SerializeField] TextMeshProUGUI ticketsArrive;
    [SerializeField] TextMeshProUGUI auxiliarModif;
    [SerializeField] Animator ticketsArrive_animator;

    bool anim = false;
    bool interestLocked;

    public static bool WinByInterest
    {
        get 
        {
            return Instance.interest / Instance.maxInterest > Instance.percentInterestToWin;
        }
       
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    public void InterestLock(bool interestLock) => interestLocked = interestLock;

    private void Start()
    {
        GameLoop.Instance.OnStartGame += BeginCountDown;

        sensor.SubscribeToInterestPoint(OnEnterInterestPoint, OnExitInterestPoint);
        img.fillAmount = 1f;

        RefreshTickets(NO_Animate: true, modif: 0);
    }

    public void OnEnterInterestPoint(InterestPoint _ip)
    {
        _ip.Enter();
        if(_ip != lastInterestPoint)
        {
            lastInterestPoint = _ip;
            _currentScore += (long)(Player.PeopleAmount * scoreMultiplier * (interest / maxInterest));
        }
    }
    public void OnExitInterestPoint(InterestPoint _ip)
    {
        _ip.Exit();
    }

    
    public void BeginCountDown()
    {
        GameLoop.Instance.OnStartGame -= BeginCountDown;

        anim = true;
        interest = maxInterest;

    }

    public void RefreshTickets(bool NO_Animate = false, int modif = 0)
    {
        ticketsArrive.text = Player.PeopleAmount.ToString();
        if (!NO_Animate) 
        {
            if (modif != 0)
            {
                bool pos = modif > 0;
                auxiliarModif.text = $"<color={(pos ? "green":"red")}>{(pos ? "+" : "-")}{modif}</color>";
            }
            else
            {
                auxiliarModif.text = string.Empty;
            }
            
            ticketsArrive_animator.Play("TicketModify"); 
        }
    }

    private void Update()
    {
        if (!anim) return;
        if(!interestLocked) RemoveInterest(quantToRemove * Time.deltaTime);

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
    public void CompleteInterest()
    {
        ScoreFeedbackManager.ShowScoreInPos("Interés lleno", Color.green, player.transform.position + Vector3.up);
        interest = maxInterest;
        Refresh();
    }

    public void RemoveInterest(float toRemove)
    {
        interest -= toRemove;
        Refresh();

        if(interest <= 0)
        {
            GameLoop.Lose(_currentScore);
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
