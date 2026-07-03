using UnityEngine;
using UnityEngine.UI;
public class SliderLife : MonoBehaviour
{
    public static SliderLife instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Image img;
    [SerializeField] private float duration = 10f;

    float interest = 100;
    [SerializeField] int maxInterest = 100;

    bool anim = false;
    public void BeginCountDown()
    {
        anim = true;
        interest = maxInterest;

    }

    private void Start()
    {
        img.fillAmount = 1f;
    }

    private void Update()
    {
        if (!anim) return;
        interest = interest - 1 * Time.deltaTime;
        Refresh();
    }

    void Refresh()
    {
        img.fillAmount = (float)interest / duration;
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
}
