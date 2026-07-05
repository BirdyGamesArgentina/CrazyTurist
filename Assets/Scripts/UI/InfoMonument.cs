using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoMonument : MonoSingleton<InfoMonument>
{


    [SerializeField] CanvasGroup group;

    [SerializeField] Image photoLeft;
    [SerializeField] Image photoRight;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] Animator myAnimBigMonument;
    [SerializeField] Animator myAnimFastPhoto;

    float timer;
    bool cd;
    [SerializeField] float time_to_show = 3f;
    public void SetInfoBigMonument(string _title, string _description, Sprite _photo, bool left = false)
    {
        title.text = _title;
        description.text = _description;

        if (left)
        {
            photoLeft.enabled = true;
            photoRight.enabled = false;
            photoLeft.sprite = _photo;
        }
        else
        {
            photoRight.enabled = true;
            photoLeft.enabled = false;
            photoRight.sprite = _photo;
        }

        myAnimBigMonument.Play("bigMonument_Appear");

        cd = true;
        timer = 0;
    }


    public void SetInfoFastPhoto(string _title, Sprite _photo)
    {
        //title.text = _title;
        //photo.sprite = _photo;

    }

    protected override void OnAwake()
    {
        
    }

    private void Update()
    {
        if (cd)
        {
            if (timer < time_to_show)
            {
                timer += Time.deltaTime;
            }
            else

            {
                myAnimBigMonument.Play("bigMonument_Disappear");
                timer = 0;
                cd = false;
            }
        }
    }
}
