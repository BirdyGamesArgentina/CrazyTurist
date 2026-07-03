using System.Linq;
using UnityEngine;

public class InterestPointStop : InterestPoint
{
    public GameObject[] countOfX;
    public int peopleToLeave;

    protected override void OnEnter()
    {
        if (PointOfInterestSystem.Instance.interest < 50)
        {
            int left = 0;

            for (int i = 0; i < Player.instance.Persons.Length && left < peopleToLeave; i++)
            {
                Transform seat = Player.instance.Persons[i];

                if (seat.childCount == 0)
                    continue;

                GameObject person = seat.GetChild(0).gameObject;

                person.transform.SetParent(null);
                person.SetActive(false);

                left++;
            }
        }
        int personIndex = 0;

        for (int i = 0; i < Player.instance.Persons.Length && personIndex < countOfX.Length; i++)
        {
            Transform seat = Player.instance.Persons[i];

            if (seat.childCount > 0)
                continue; 
            countOfX[personIndex].transform.SetParent(seat, false);
            countOfX[personIndex].transform.localPosition = Vector3.zero;

            personIndex++;
        }
    }
    protected override void OnExit()
    {
        
    }

    protected override void OnFinishVisit()
    {
        
    }
}
