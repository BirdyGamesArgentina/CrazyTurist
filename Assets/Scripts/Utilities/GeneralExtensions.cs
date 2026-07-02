using UnityEngine;

public static class GeneralExtensions
{
    public static T CreateDefaultSubObject<T>(this GameObject owner, string name) where T : Component
    {
        GameObject go = new GameObject();
        go.name = name;
        T back = go.AddComponent<T>();
        go.transform.SetParent(owner.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = new Vector3(1, 1, 1);
        return back;
    }
}
