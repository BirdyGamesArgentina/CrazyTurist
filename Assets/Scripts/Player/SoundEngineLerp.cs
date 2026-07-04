using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class SoundEngineLerp : MonoSingleton<SoundEngineLerp>
{
    [SerializeField] AudioSource slow;
    [SerializeField] AudioSource average;
    [SerializeField] AudioSource fast;
    protected override void OnAwake() { }

    public static void MainLerp(float lerp) => Instance._MainLerp(lerp);
    void _MainLerp(float lerp)
    {
        lerp = Mathf.Clamp01(lerp);

        slow.volume =
            Mathf.Clamp01(1f - Mathf.Abs(lerp - 0f) / 0.5f);

        average.volume =
            Mathf.Clamp01(1f - Mathf.Abs(lerp - 0.5f) / 0.5f);

        fast.volume =
            Mathf.Clamp01(1f - Mathf.Abs(lerp - 1f) / 0.5f);
    }

}
