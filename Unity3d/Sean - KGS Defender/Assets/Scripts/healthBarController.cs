using UnityEngine;
using System.Collections;

public class healthBarController : MonoBehaviour {

    public GameObject cam;
    public Vector3 fullScale;
    public float healthBarEmptySpeed;

    Stats stat;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("MainCamera");
        stat = this.gameObject.transform.parent.gameObject.GetComponent<Stats>();
        fullScale = gameObject.transform.localScale;
       
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(cam.gameObject.transform);
       Vector3 currScale = this.gameObject.transform.localScale;
        Vector3 destScale = new Vector3(fullScale.x * (stat.health / 100), fullScale.y * (stat.health / 100), fullScale.z);
        Vector3 target = Vector3.Lerp(currScale, destScale, healthBarEmptySpeed);
        target.x = Mathf.Clamp(target.x, 0, fullScale.x);
        target.y = Mathf.Clamp(target.y, 0, fullScale.y);
        gameObject.transform.localScale = target;
	}
}
