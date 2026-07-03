using UnityEngine;

public class OnEndScreenResult
{
    public string Title { get; private set; }
    public int Score { get; private set; }

    public OnEndScreenResult(string title, int score)
    {
        Title = title;
        Score = score;
    }

}
