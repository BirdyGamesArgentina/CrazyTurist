using UnityEngine;

public class PeopleSkinSelector : MonoBehaviour
{
    public Material[] peopleSkins;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        GetComponent<Renderer>().material = peopleSkins[Random.Range(0, peopleSkins.Length)];
    }
    // Update is called once per frame
    void Update()
    {
        

    }
}
