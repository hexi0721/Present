using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {

        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public AudioClip BackGroundAudio , OpenAudio ;
    public AudioSource efxsource;

    public void PlayAudio(AudioClip Clip)
    {
        efxsource.clip = Clip;

        efxsource.PlayOneShot(Clip);
    }


    
}
