using UnityEngine;
using System.Collections;

public class PlayIntroMovie : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        gameObject.GetComponent<AudioSource>().PlayDelayed(.5f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
