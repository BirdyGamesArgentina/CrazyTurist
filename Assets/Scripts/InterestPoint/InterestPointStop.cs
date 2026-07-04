using System.Linq;
using UnityEngine;

public class InterestPointStop : InterestPoint
{

    [SerializeField] ProgressionModule progressionModule;

    [Header("Lógica de subida de personas")]
    public GameObject[] countOfX;
    public int peopleToLeave;

    private void Awake()
    {
        progressionModule.SetCallbackOnFinish(FinishVisit);
    }

    protected override void OnEnter()
    {
        progressionModule.Begin();
    }
    protected override void OnExit()
    {
        progressionModule.End();
    }

    protected override void OnFinishVisit()
    {
        int modif = 0;
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

            modif -= left;
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
        modif += personIndex;

        PointOfInterestSystem.Instance.RefreshTickets(NO_Animate: false, modif: modif);
    }
}
