using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    C4,
    D4,
    E4,
    F4,
    G4,
    A5,
    B5,
    C5,
    FINISHED,
}

public class SoundCollection
{
    public AudioClip[] clips;
    private int lastClipIndex;

    public SoundCollection(params string[] clipNames)
    {
        this.clips = new AudioClip[clipNames.Length];
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i] = Resources.Load<AudioClip>(clipNames[i]);
            if (clips[i] == null)
            {
                Debug.Log($"can't find audio clip {clipNames[i]}");
            }
        }
        lastClipIndex = -1;
    }

    public AudioClip GetRandClip()
    {
        if (clips.Length == 0)
        {
            Debug.Log("No clips to give");
            return null;
        }
        else if (clips.Length == 1)
        {
            return clips[0];
        }
        else
        {
            int index = lastClipIndex;
            while (index == lastClipIndex)
            {
                index = Random.Range(0, clips.Length);
            }
            lastClipIndex = index;
            return clips[index];
        }
    }

}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public float mainVolume = 1.0f;
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSrc;

    public static SoundManager Instance { get; private set; }

    // unity life cycle
    private void Awake()
    {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
        sounds = new() {
            { SoundType.C4, new("C4") },
            { SoundType.D4, new("D4") },
            { SoundType.E4, new("E4") },
            { SoundType.F4, new("F4") },
            { SoundType.G4, new("G4") },
            { SoundType.A5, new("A5") },
            { SoundType.B5, new("B5") },
            { SoundType.C5, new("C5") },
            { SoundType.FINISHED, new("CounterPoint") },
        };
    }

    public static void Play(SoundType type, AudioSource audioSrc = null, float pitch = -1)
    {
        print("playing sound");
        if (Instance.sounds.ContainsKey(type))
        {
            audioSrc ??= Instance.audioSrc;
            audioSrc.volume = Random.Range(0.70f, 1.0f) * Instance.mainVolume;
            audioSrc.pitch = pitch >= 0 ? pitch : Random.Range(0.75f, 1.25f);
            audioSrc.clip = Instance.sounds[type].GetRandClip();
            audioSrc.Play();
        }
    }
}