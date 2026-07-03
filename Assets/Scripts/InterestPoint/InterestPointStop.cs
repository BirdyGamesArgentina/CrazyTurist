using System.Linq;
using UnityEngine;

public class InterestPointStop : InterestPoint
{
    public GameObject[] countOfX;

    protected override void OnEnter()
    {
        int count = Mathf.Min(countOfX.Length, Player.instance.Persons.Length);

        for (int i = 0; i < count; i++)
        {
            countOfX[i].transform.position = Player.instance.Persons[i].position;
            countOfX[i].transform.SetParent(Player.instance.Persons[i], false);

            countOfX[i].transform.localPosition = Vector3.zero;
        }
    }
    protected override void OnExit()
    {
        
    }

    protected override void OnFinishVisit()
    {
        
    }
}
