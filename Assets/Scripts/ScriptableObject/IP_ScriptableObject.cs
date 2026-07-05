using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "IP_ScriptableObject", menuName = "Scriptable Objects/monuments")]
public class IP_ScriptableObject : ScriptableObject
{
    public string nameOfMonument;

    [TextArea(3, 10)]
    public string informationText;
    public Sprite image;

   // public GameObject animation_Transform;

}
