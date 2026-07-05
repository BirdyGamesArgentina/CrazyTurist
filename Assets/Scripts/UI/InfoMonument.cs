using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoMonument : MonoSingleton<InfoMonument>
{


    [Header("Big Monument")]
    [SerializeField] CanvasGroup group;
    [SerializeField] Image photoLeft;
    [SerializeField] Image photoRight;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Animator myAnimBigMonument;
    float timer_monument;
    bool cd_monument;
    [SerializeField] float time_to_show_monument = 3f;

    [Header("FastPhoto")]
    
    [SerializeField] Animator myAnimFastPhoto;
    [SerializeField] TextMeshProUGUI title_F_photo;
    bool animReveal;
    float reveal_timer;
    [SerializeField] Image fast_photo;
    [SerializeField] Image blackReveal;
    [SerializeField] float time_to_reveal = 2f;
    float timer_FPhoto;
    bool cd_FPhoto;
    [SerializeField] float time_to_show_Fphoto = 3f;


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

        cd_monument = true;
        timer_monument = 0;
    }

    public void SetInfoFastPhoto(string _title, Sprite _photo)
    {
        title_F_photo.text = _title;
        fast_photo.sprite = _photo;
        reveal_timer = 0;
        animReveal = true;
        myAnimFastPhoto.Play("fastPhoto_Appear");
    }

    protected override void OnAwake()
    {
        
    }

    Color transparent = new Color(0,0,0,0);
    private void Update()
    {
        if (animReveal)
        {
            if (reveal_timer < time_to_reveal)
            {
                reveal_timer += Time.deltaTime;
                blackReveal.color = Color.Lerp(Color.black, transparent, reveal_timer / time_to_reveal);
            }
            else
            {
                animReveal = false;
                reveal_timer = 0;
                cd_FPhoto = true;
                timer_FPhoto = 0f;
            }
        }

        if (cd_monument)
        {
            if (timer_monument < time_to_show_monument)
            {
                timer_monument += Time.deltaTime;
            }
            else
            {
                myAnimBigMonument.Play("bigMonument_Disappear");
                timer_monument = 0;
                cd_monument = false;
            }
        }

        if (cd_FPhoto)
        {
            if (timer_FPhoto < time_to_show_Fphoto)
            {
                timer_FPhoto += Time.deltaTime;
            }
            else
            {
                myAnimFastPhoto.Play("fastPhoto_Disappear");
                timer_FPhoto = 0;
                cd_FPhoto = false;
            }
        }
    }
}
