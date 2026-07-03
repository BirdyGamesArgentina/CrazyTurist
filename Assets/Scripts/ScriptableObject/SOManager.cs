using TMPro;
using UnityEngine;

public class SOManager : MonoBehaviour
{

    public TextMeshProUGUI myNameOnCanva, MyInformationInCanva;
    public GameObject myImage;
    public static SOManager instance;


    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
