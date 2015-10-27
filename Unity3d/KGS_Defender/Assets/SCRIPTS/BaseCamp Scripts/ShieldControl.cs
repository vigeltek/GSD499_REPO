using UnityEngine;
using System.Collections;

public class ShieldControl : MonoBehaviour
{
    public GameObject shieldGen;
    public ParticleSystem particleEffect;
    private bool shieldOff = false;
    private Color lerpColor;
    private float shieldHealth;

	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Transform>().Rotate(0,Time.deltaTime * 15  ,0);

        shieldHealth = gameObject.GetComponent<Stats>().health;

        if (shieldHealth <= 0 && shieldOff == false)
        {
            shieldOff = true;
            
            shieldGen.GetComponent<GeneratorControl>().ShieldDown(true);
            gameObject.GetComponent<Animator>().SetBool("Shield Down", true);
            gameObject.SetActive(false);
            Destroy(particleEffect);
            Destroy(gameObject, 0.5f);        
        }

        GetLerpColor();

	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Enemy Attack"))
        {
            StartCoroutine(FlashShield());
        }
    }

    void OnTriggerExit()
    {
        gameObject.GetComponent<Renderer>().material.color = lerpColor;
    }

    void GetLerpColor()
    {
        float temp;
        temp = shieldHealth / GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().shieldHealth;

        lerpColor = Color.Lerp(Color.red, Color.green, temp);
        if (particleEffect != null)
        { 
            particleEffect.GetComponent<ParticleSystem>().startColor = lerpColor;
        }
    }

    IEnumerator FlashShield()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        yield return new WaitForSeconds(.025f);
        gameObject.GetComponent<Renderer>().material.color = lerpColor;

    }
}
