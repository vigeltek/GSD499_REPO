using UnityEngine;
using System.Collections;

public class PlayIntroMovie : MonoBehaviour {
    private MovieTexture movie;
    // Use this for initialization
    void Start()
    {
        movie = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        movie.Play();
        gameObject.GetComponent<AudioSource>().PlayDelayed(.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (!movie.isPlaying)
        {
            Debug.Log("Intro call");

            Application.LoadLevel("OpeningVideo");
        }
	}
}
