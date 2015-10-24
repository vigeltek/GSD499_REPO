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
        gameObject.GetComponent<Transform>().Rotate(0,Time.deltaTime * 90  ,0);

        shieldHealth = gameObject.GetComponent<Stats>().health;

        if (shieldHealth <= 0 && shieldOff == false)
        {
            shieldOff = true;
            
            shieldGen.GetComponent<GeneratorControl>().ShieldDown(true);
            gameObject.GetComponent<Animator>().SetBool("Shield Down", true);
            gameObject.active = false;
            Destroy(particleEffect);
            Destroy(gameObject, 0.5f);        
        }

        GetLerpColor();

	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Enemy Attack"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            gameObject.GetComponent<Renderer>().material.color = lerpColor;
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
}
