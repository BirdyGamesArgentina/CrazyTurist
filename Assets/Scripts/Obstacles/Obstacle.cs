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


    private void Start()
    {
        Invoke("OnStart", 0.5f);

        foreach (var op in options)
        {
            op.SetActive(false);
        }
        var selected = options[Random.Range(0, options.Length)];
        selected.SetActive(true);

        modelToScale = selected.transform;
        min = modelToScale.localScale - offset;
        max = modelToScale.localScale + offset;

        rend = selected.GetComponent<Renderer>();
        ChangeRandomColor();
    }

    void ChangeRandomColor()
    {
        rend.material.SetColor(VERTEXTCOLOR, Random.ColorHSV());
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


    Transform modelToScale;
    Renderer rend;
    const string VERTEXTCOLOR = "_VertexColor";
    Color current = Color.white;

    Vector3 offset = new Vector3(0.05f, 0.05f, 0.05f);
    Vector3 min = new Vector3(0.05f, 0.05f, 0.05f);
    Vector3 max = new Vector3(0.05f, 0.05f, 0.05f);
    void Update()
    {
//        transform.forward = direction;

        if (inCD)
        {
            if (timercd < cd)
            {
                timercd += Time.deltaTime;

                float curveVal = curve.Evaluate(timercd / cd);

                modelToScale.localScale = Vector3.Lerp(min, max, curveVal);

                current = rend.material.GetColor(VERTEXTCOLOR);
                current.a = curveVal;
                rend.material.SetColor(VERTEXTCOLOR, current);
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

    public void Explode()
    {
        Destroy(gameObject);
    }

}
