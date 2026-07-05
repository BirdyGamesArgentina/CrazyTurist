using UnityEngine;
using UnityEngine.UI;

public class GenericBar : MonoSingleton<GenericBar>
{
    [SerializeField] CanvasGroup group;
    [SerializeField] Image img;
    protected override void OnAwake()
    {

    }

    private void Start()
    {
        group.alpha = 0;
    }

    public static void SetValue(float val)
    {
        Instance.img.fillAmount = val;
    }

    public static void Show(bool val)
    {
        Instance.group.alpha = val ? 1 : 0;
    }

    
}
