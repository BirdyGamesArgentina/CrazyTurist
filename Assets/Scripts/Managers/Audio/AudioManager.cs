using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;


public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] AudioMixerGroup mainMixer = null;
    public enum SoundDimesion { ThreeD, TwoD }
    public enum OverlapMode { None, DontDisturb, CancelCurrent }
    public enum SoundPriority { Medium = 75, High = 0, Low = 150 }

    private Dictionary<string, List<AudioSource>> _soundRegistry = new Dictionary<string, List<AudioSource>>();
    [SerializeField] int pool2DAmmount = 10;
    [SerializeField] int pool3DAmmount = 10;

    [SerializeField] AudioSource[] ambientalSounds = new AudioSource[0];

    ObjectPool<AudioSource> pool = null;

    protected override void OnAwake()
    {
        pool = new ObjectPool<AudioSource>
                (
                 createFunc: () =>
                 {
                     Transform cam = AudioManager.Instance.transform;

                     var source = cam
                         .gameObject
                         .CreateDefaultSubObject<AudioSource>("SOURCE-> " + name);

                     source.spatialBlend = 0;

                     return source;
                 },
                 actionOnGet: x => x.gameObject.SetActive(true),
                 actionOnRelease: x => x.gameObject.SetActive(false),
                 actionOnDestroy: x => Destroy(x.gameObject),
                 defaultCapacity: pool2DAmmount
                );
    }


    #region TO USE
    public AudioSource PlaySound(string soundPoolName, AudioClip clip, bool loop = false, OverlapMode overlapMode = OverlapMode.None, AudioMixerGroup overrideMixer = null, SoundPriority soundPriority = SoundPriority.Medium, Transform trackingTransform = null, Action calbackEnd = null)
    {
        if (overlapMode == OverlapMode.DontDisturb && (_soundRegistry.ContainsKey(soundPoolName) && _soundRegistry[soundPoolName].Count > 0)) return null;

        if(overlapMode == OverlapMode.CancelCurrent) 
            StopAllSounds(soundPoolName);

        AudioSource aS = pool.Get();
        aS.clip = clip;
        aS.outputAudioMixerGroup = overrideMixer != null ? overrideMixer : mainMixer;
        aS.loop = loop;

        if (aS == null)
        {
            return null;
        }

        aS.priority = (int)soundPriority;

        if (trackingTransform != null) aS.transform.position = trackingTransform.position;
        aS.Play();

        if (!aS.loop)
            StartCoroutine(ReturnSoundToPool(aS, soundPoolName, calbackEnd));


        if (_soundRegistry.ContainsKey(soundPoolName))
        {
            _soundRegistry[soundPoolName].Add(aS);
        }
        else
        {
            _soundRegistry.Add(soundPoolName, new List<AudioSource>() { aS });
        }

        return aS;
    }
    public void StopAllSounds(string soundPoolName = null)
    {
        if (soundPoolName == null)
        {
            foreach (var aS in _soundRegistry)
            {
                for (int i = 0; i < aS.Value.Count; i++)
                {
                    aS.Value[i].Stop();
                    pool.Release(aS.Value[i]);
                }
            }
            _soundRegistry.Clear();
        }
        else if (_soundRegistry.ContainsKey(soundPoolName))
        {
            for (int i = 0; i < _soundRegistry[soundPoolName].Count; i++)
            {
                _soundRegistry[soundPoolName][i].Stop();
                pool.Release(_soundRegistry[soundPoolName][i]);
            }

            _soundRegistry[soundPoolName].Clear();
        }
        else
        {
            Debug.LogWarning("No tenes ese sonido en en pool");
        }
    }

    public void StopSound(AudioSource source, string soundPoolName)
    {
        if (_soundRegistry.ContainsKey(soundPoolName) && _soundRegistry[soundPoolName].Contains(source))
        {
            source.Stop();

            pool.Release(source);

            _soundRegistry[soundPoolName].Remove(source);
        }
    }

    public void PauseSounds()
    {
        foreach (var aS in _soundRegistry)
        {
            for (int i = 0; i < aS.Value.Count; i++)
            {
                aS.Value[i].Pause();
            }
        }
        for (int i = 0; i < ambientalSounds.Length; i++)
        {
            if (ambientalSounds[i].isPlaying)
                ambientalSounds[i].Pause();
        }
    }
    public void ResumeSounds()
    {
        foreach (var aS in _soundRegistry)
        {
            for (int i = 0; i < aS.Value.Count; i++)
            {
                aS.Value[i].Play();
            }
        }
        for (int i = 0; i < ambientalSounds.Length; i++)
        {
            ambientalSounds[i].Play();
        }
    }
    #endregion

    #region Privates

    IEnumerator ReturnSoundToPool(AudioSource aS, string sT, Action EndCallback = null)
    {
        yield return new WaitForSeconds(aS.clip.length);

        EndCallback?.Invoke();
        pool.Release(aS);
        _soundRegistry[sT].Remove(aS);
    }
    #endregion


    public void DeleteSoundPool(string soundPoolName)
    {
        //Destroy(_soundRegistry[soundPoolName].gameObject);
        //_soundRegistry.Remove(soundPoolName);
    }

    public void ChangePitch(float val)
    {
        mainMixer.audioMixer.SetFloat("MyPitch", val);
    }
}
