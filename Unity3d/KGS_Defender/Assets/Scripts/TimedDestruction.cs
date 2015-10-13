using UnityEngine;
using System.Collections;

public class TimedDestruction : MonoBehaviour {

    public float timer; 

	// Use this for initialization
	void Start () {

        Destroy(this.gameObject, timer);
	}

}
