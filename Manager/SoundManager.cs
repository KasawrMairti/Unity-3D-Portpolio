using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GameManager<SoundManager>
{
    private AudioSource audioSource;
    private AudioSource audioSourceOneShot;

    [SerializeField] private SoundDictionary soundBGM;
    [SerializeField] private SoundDictionary soundSE;

    [SerializeField] private Dictionary<string, AudioClip> dictionaryBGM = new Dictionary<string, AudioClip>();
    [SerializeField] private Dictionary<string, AudioClip> dictionarySE = new Dictionary<string, AudioClip>();


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourceOneShot = gameObject.transform.Find("PlayOneShot").GetComponent<AudioSource>();

        dictionaryBGM = soundBGM.ToDictionary();
        dictionarySE = soundSE.ToDictionary();
    }

    public void PlayBGM(string name)
    {
        foreach (string key in dictionaryBGM.Keys)
        {
            if (key == name)
            {
                audioSource.Stop();
                audioSource.clip = dictionaryBGM[name];
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlaySE(string name)
    {
        foreach (string key in dictionarySE.Keys)
        {
            if (key == name)
            {
                audioSourceOneShot.Stop();
                audioSourceOneShot.pitch = 1.0f;
                audioSourceOneShot.loop = false;
                audioSourceOneShot.PlayOneShot(dictionarySE[name]);
                audioSource.pitch = -1.0f;
            }
        }
    }

    public void StopSE()
    {
        audioSourceOneShot.Stop();
    }
}

[Serializable]
public class SoundDictionary
{
    [SerializeField] public List<SoundSource> soundSource = new List<SoundSource>();

    public Dictionary<string, AudioClip> ToDictionary()
    {
        Dictionary<string, AudioClip> newDic = new Dictionary<string, AudioClip>();

        foreach (SoundSource item in soundSource)
        {
            newDic.Add(item.name, item.sound);
        }

        return newDic;
    }


}

[Serializable]
public class SoundSource
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip sound;
}