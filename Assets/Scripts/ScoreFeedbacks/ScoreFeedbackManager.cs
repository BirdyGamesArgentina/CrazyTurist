using UnityEngine;
using UnityEngine.Pool;

public class ScoreFeedbackManager : MonoBehaviour
{
    public static ScoreFeedbackManager Instance;
    [SerializeField] ScoreItem model;

    ObjectPool<ScoreItem> pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        pool = new ObjectPool<ScoreItem>
            (
                createFunc: () =>
                {
                    ScoreItem s = GameObject.Instantiate(model, this.transform);
                    s.SetOnFinish(OnRelease);
                    return s;
                },
                actionOnGet: x =>
                {
                    x.gameObject.SetActive(true);

                },
                actionOnRelease: x =>
                {
                    x.gameObject.SetActive(false);
                }
                ,
                actionOnDestroy: x =>
                {
                    GameObject.Destroy(x.gameObject);
                }
            );
    }

    void OnRelease(ScoreItem s)
    {
        pool.Release(s);
    }

    public static void ShowScoreInPos(string value, Color color, Vector3 position)
    {
        Instance._showScoreInPosition(value,color,position);
    }
    void _showScoreInPosition(string value, Color color, Vector3 position)
    {
        var item = pool.Get();
        item.transform.position = position;
        item.Animate(value, color);
    }
}
