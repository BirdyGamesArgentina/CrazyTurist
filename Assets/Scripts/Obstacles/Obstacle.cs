using PathFinding;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 direction;
    [SerializeField] float obstacleSpeed;
    [SerializeField] Rigidbody myRb;

    [SerializeField] bool applyKnockBack = true;
    [SerializeField] float knockbackForce = 5f;

    bool inCD = false;
    [SerializeField] float cd = 1f;
    float timercd;

    [SerializeField] int interest = 5;

    [SerializeField] AnimationCurve curve;
    [SerializeField] Renderer[] renders;

    //[SerializeField] Transform destiny;
    [SerializeField] PathFinder pathfinder;

    [SerializeField] GameObject[] options;

    public int InterestToRemove
    {
        get 
        {
            return interest;
        }
    }

    private void Awake()
    {
        renders = GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        Invoke("OnStart", 0.5f);

        foreach (var op in options)
        {
            op.SetActive(false);
        }
        options[Random.Range(0, options.Length)].SetActive(true);
    }

    void OnStart()
    {
        pathfinder.CallbackOnFinish(OnReachPOsition);
        pathfinder.GoToPosition(PathFindingOptions.GetRandomPosition());
    }

    void OnReachPOsition()
    {
        pathfinder.GoToPosition(PathFindingOptions.GetRandomPosition());
    }

    const string COLOR_PROP = "_Color";
    Color current = Color.white;
    void Update()
    {
//        transform.forward = direction;

        if (inCD)
        {
            if (timercd < cd)
            {
                timercd += Time.deltaTime;

                float curveVal = curve.Evaluate(timercd / cd);

                for (int i = 0; i < renders.Length; i++)
                {
                    current = renders[i].material.GetColor(COLOR_PROP);
                    current.a = curveVal;
                    renders[i].material.SetColor(COLOR_PROP, current);
                }
            }
            else
            {
                timercd = 0f;
                inCD = false;
                pathfinder.Stunned(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (inCD) return;
        //myRb.linearVelocity = direction * Time.deltaTime * obstacleSpeed;
    }

    public void HitKnockback(Vector3 dir)
    {
        if (inCD) return;
        
        inCD = true;

        pathfinder.Stunned(true);

        if (applyKnockBack)
        {
            myRb.linearVelocity = Vector3.zero;
            myRb.AddForce(dir * knockbackForce, ForceMode.VelocityChange); 
        }
    }

}
