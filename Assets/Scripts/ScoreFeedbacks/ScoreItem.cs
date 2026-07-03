using System;
using TMPro;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public Animator animator;
    [SerializeField] TextMeshPro tmp;

    Action<ScoreItem> onfinish;

    public void SetOnFinish(Action<ScoreItem> onFinish)
    {
        this.onfinish = onFinish;
    }

    public void Animate(string value, Color color)
    {
        tmp.text = value;
        tmp.color = color;
        animator.Play("ScoreFeedback");
    }

    public void ANIM_EVENT_FINISH()
    {
        onfinish.Invoke(this);
    }
}
