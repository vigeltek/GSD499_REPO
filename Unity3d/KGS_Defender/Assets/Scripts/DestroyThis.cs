using UnityEngine;
using System.Collections;

public class DestroyThis : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 3f);
    }
}
