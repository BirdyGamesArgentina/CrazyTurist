using UnityEngine;
using UnityEngine.UI;
public class SliderLife : MonoBehaviour
{
    public static SliderLife instance;
    private void Awake()
    {
        instance = this;
    }

    
}
