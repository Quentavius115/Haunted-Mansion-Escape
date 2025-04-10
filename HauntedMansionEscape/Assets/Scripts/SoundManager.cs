using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum SoundType
{
    AMBIENCE,
    FLAME_IGNITE,
    FLAME_CHANGE,
    PIANOFINISH,
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


// I dont think much needs to be changed from the original file cherry made, just adding our own sounds.
// Credit to this code is to cherry of course

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
            { SoundType.AMBIENCE, new("Ambience/Thunder_1","Ambience/Thunder_2","Ambience/Thunder_3","Ambience/Thunder_4","Ambience/Spooky_1","Ambience/Spooky_2") },
            { SoundType.FLAME_IGNITE, new("Candle_Light") },
            { SoundType.FLAME_CHANGE, new("Candle_Change") },
            { SoundType.PIANOFINISH, new("CounterPoint") },
        };
    }

    public void Play(SoundType type, AudioSource audioSrc = null)
    {
        if (sounds.ContainsKey(type))
        {
            audioSrc ??= this.audioSrc;
            audioSrc.volume = Random.Range(0.40f, 0.70f) * mainVolume;
            audioSrc.pitch = Random.Range(0.5f, 1.25f);
            audioSrc.clip = sounds[type].GetRandClip();
            audioSrc.Play();
        }
    }
}