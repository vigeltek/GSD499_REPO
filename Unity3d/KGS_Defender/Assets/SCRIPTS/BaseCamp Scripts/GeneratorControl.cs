using UnityEngine;
using System.Collections;

public class GeneratorControl : MonoBehaviour
{
    private bool shieldDown;
    public GameObject explosion;

	// Use this for initialization
	void Start ()
    {
        shieldDown = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(shieldDown == true)
        {
            GameObject FXClone = (GameObject)Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    public void ShieldDown(bool down)
    {
        shieldDown = down;
    }
}
