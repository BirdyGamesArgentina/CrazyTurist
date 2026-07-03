using UnityEngine;

public class OnEndScreenResult
{
    public string Title { get; private set; }
    public long Score { get; private set; }

    public OnEndScreenResult(string title, long score)
    {
        Title = title;
        Score = score;
    }

}
