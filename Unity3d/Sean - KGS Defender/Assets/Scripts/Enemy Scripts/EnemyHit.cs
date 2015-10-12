using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour
{
    public GameObject hitFX;
    public AudioClip hitSFX;

	// Use this for initialization
	void OnTriggerEnter (Collider c)
    {
        if(c.gameObject.CompareTag ("Base Component"))
        {
            GameObject hitFXclone = (GameObject)Instantiate(hitFX, gameObject.transform.position, gameObject.transform.rotation);

            DestroyProjectile(gameObject, 0f);

            DestroyProjectile(hitFXclone, 2f);
        }
    }

    void DestroyProjectile(GameObject toDestroy, float timer)
    {
        Destroy(toDestroy, timer);
    }
	

}
