using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFX : MonoSingleton<SoundFX>
{
    [SerializeField] SoundProperties[] sounds = new SoundProperties[0];
    protected override void OnAwake()
    {
    }
    public static AudioSource PlaySound(string ID, AudioManager.OverlapMode overlap = AudioManager.OverlapMode.None)
    {
        if (ID == null) return null;

        var properties = Instance.SearchByID(ID);

        if (properties.sounds == null)
        {
            Debug.LogWarning("No tenes seteado el sonido");
            return null;
        }

        return AudioManager.Instance.PlaySound(properties.ID, properties.sounds[Random.Range(0, properties.sounds.Length)], properties.isLoop, overlap, properties.group, properties.priority);
    }

    public static void StopSound(string ID)
    {
        AudioManager.Instance.StopAllSounds(ID);
    }

    public SoundProperties SearchByID(string ID)
    {
        SoundProperties result = new SoundProperties();
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].ID == ID)
            {
                result = sounds[i];
                break;
            }
        }

        return result;
    }
}

[System.Serializable]
public struct SoundProperties
{
    public string ID;
    public AudioClip[] sounds;
    [HideInInspector] public int currentIndex;
    public AudioManager.SoundPriority priority;
    public bool isLoop;
    [SerializeField] internal AudioMixerGroup group;
}
