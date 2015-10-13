using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    private List<AudioSource> audioSourceList = new List<AudioSource>();

    private static float musicVolume = .75f;
    private static float sfxVolume = .75f;


    private static AudioManager instance;
    private GameObject thisObj;
    private Transform thisT;

    public static void Init()
    {

        if (instance != null) return;
        Debug.Log("Loading Audio Manager");
        GameObject obj = new GameObject();
        obj.name = "AudioManager";
        obj.AddComponent<AudioManager>();
    }


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        thisObj = gameObject;
        thisT = transform;

        DontDestroyOnLoad(thisObj);


        audioSourceList = new List<AudioSource>();
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = new GameObject();
            obj.name = "AudioSource" + (i + 1);

            AudioSource src = obj.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.loop = false;

            obj.transform.parent = thisT;
            obj.transform.localPosition = Vector3.zero;

            audioSourceList.Add(src);
        }

        AudioListener.volume = sfxVolume;
    }

    //check for the next free, unused audioObject
    private int GetUnusedAudioSourceID()
    {
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            if (!audioSourceList[i].isPlaying) return i;
        }
        return 0;   //if everything is used up, use item number zero
    }

    //call to play a specific clip
    public static void PlaySound(AudioClip clip)
    {
        if (instance == null) Init();
        instance._PlaySound(clip);
    }
    public void _PlaySound(AudioClip clip)
    {
        int ID = GetUnusedAudioSourceID();

        audioSourceList[ID].clip = clip;
        audioSourceList[ID].Play();
    }

    public static void SetSFXVolume(float val)
    {
        sfxVolume = val;
        AudioListener.volume = val;
    }

    public static void SetMusicVolume(float val)
    {
        //musicVolume=val;
        //if(instance && instance.musicSource) instance.musicSource.volume=val;
    }

    public static float GetMusicVolume() { return musicVolume; }
    public static float GetSFXVolume() { return sfxVolume; }


}
