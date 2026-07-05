using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpsUI : MonoSingleton<PowerUpsUI>
{
    protected override void OnAwake()
    {
        
    }

    [SerializeField] CanvasGroup group;
    [SerializeField] Image img_front;
    [SerializeField] Image img_back;
    [SerializeField] TextMeshProUGUI description;

    public static void UpdateValue(float _value)
    {
        Instance.img_front.fillAmount = _value;
    }
    public static void Hide()
    {
        Instance.group.alpha = 0;
    }
    public static void SetValues(string _desc, Sprite _img) => Instance._setValues(_desc, _img);
    void _setValues(string _desc, Sprite _img)
    {
        group.alpha = 1;
        img_front.sprite = _img;
        img_back.sprite = _img;
        description.text = _desc;
    }

}
