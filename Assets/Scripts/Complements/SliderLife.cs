using UnityEngine;
using UnityEngine.UI;
public class SliderLife : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject sliderFill;
    [SerializeField] private float duration = 10f;

    private void Start()
    {
        slider.value = slider.maxValue;
    }

    private void Update()
    {
        slider.value -= (slider.maxValue / duration) * Time.deltaTime;

        if (slider.value <= 0)
        {
            slider.value = 0;
            sliderFill.SetActive(false);
            Debug.Log("eVento de perdiste");
        }
    }
}
