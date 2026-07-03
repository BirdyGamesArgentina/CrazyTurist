using System.Linq;
using UnityEngine;

public class InterestPointStop : InterestPoint
{
    public GameObject[] countOfX;
    public int peopleToLeave;

    bool anim = false;
    bool animOneshot = false;
    [SerializeField] float timeToFinish = 1f;
    float timer = 0f;
    [SerializeField] LerpedObject[] lerpedObject;

    protected override void OnEnter()
    {
        if(!animOneshot) anim = true;
    }
    protected override void OnExit()
    {
        if (!animOneshot) anim |= false;
    }

    private void Update()
    {
        if (anim)
        {
            if (timer < timeToFinish)
            {
                timer += Time.deltaTime;
                for (int i = 0; i < lerpedObject.Length; i++)
                {
                    lerpedObject[i].OnLerp(timer / timeToFinish);
                }
            }
            else
            {
                FinishVisit();
                anim = false;
                timer = 0;
                animOneshot = true;
            }
        }
    }

    protected override void OnFinishVisit()
    {
        if (PointOfInterestSystem.Interest < 50)
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
}
