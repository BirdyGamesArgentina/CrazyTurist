using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoSingleton<MusicHandler>
{
    [SerializeField] AudioSource[] parallels;
    Dictionary<string, AudioSource> sources;

    AudioSource current;
    AudioSource to;

    [SerializeField] bool playFirst;
    protected override void OnAwake()
    {
        sources = new Dictionary<string, AudioSource>();
        for (int i = 0; i < parallels.Length; i++)
        {
            string key = parallels[i].clip.name;
            AudioSource val = parallels[i];
            if (sources.ContainsKey(key)) throw new System.Exception("Hay dos sources con clips que se llaman igual");
            sources.Add(key, val);
        }

        current = null;
        to = null;

        if (playFirst)
        {
            current = parallels[0];
            current.Play();
            current.volume = 1f;
        }
    }
    public static void ChangeTo(string key)
    {
        Instance._changeTo(key);
    }

    void _changeTo(string key)
    {
        if (sources.ContainsKey(key))
        {
            anim = true;
            timer = 0;
            to = sources[key];
            to.volume = 0;
            to.Play();
        }
        else
        {
            throw new System.Exception("No tengo una musica con esa llaves");
        }
    }

    bool anim = false;
    [SerializeField] float time_to_lerp;
    float timer = 0;
    private void Update()
    {
        if (!anim) return;

        if (timer < time_to_lerp)
        {
            timer += Time.deltaTime;
            float lerp = timer / time_to_lerp;
            current.volume = 1 - lerp;
            to.volume = lerp;

        }
        else
        {
            anim = false;
            timer = 0;
            current.Pause();
            current = to;
            to = null;
        }
        
    }

   
}
