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
        gameObject.GetComponent<Transform>().Rotate(0, -(Time.deltaTime * 360), 0);

        if(shieldDown == true)
        {
            GameObject FXClone = (GameObject)Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }

    public void ShieldDown(bool down)
    {
        shieldDown = down;
    }
}
