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
}
