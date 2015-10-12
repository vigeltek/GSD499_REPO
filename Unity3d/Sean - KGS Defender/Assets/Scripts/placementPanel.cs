using UnityEngine;
using System.Collections;

public class placementPanel : MonoBehaviour {

    public GameObject currPrefab;

    public TPManager tpm;

    public MeshRenderer rend;

	// Use this for initialization
	void Start () {
       rend = this.gameObject.GetComponent<MeshRenderer>();
        tpm = GameObject.FindWithTag("TPM").GetComponent<TPManager>();
        BroadcastMessage("GridVisibility", false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (tpm.placementMode)
        {
            if (currPrefab == null)
            {
                rend.material.SetColor("_Color", tpm.highlightColor);

            }
            else
            {
                rend.material.SetColor("_Color", tpm.invalidColor);
            }
        }
    }

    void OnMouseExit()
    {
        
        rend.material.SetColor("_Color", tpm.normalColor);
    }

    void OnMouseDown()
    {
        if (tpm.placementMode && currPrefab == null)
        {
            currPrefab = (GameObject)Instantiate(tpm.GetTurret(), transform.position, transform.rotation);
            currPrefab.transform.parent = this.gameObject.transform;
        }
        if(tpm.destroyMode && currPrefab != null)
        {
            Destroy(currPrefab);
            currPrefab = null;
        }

    }

    public void GridVisibility(bool isvisible)
    {
        MeshRenderer render = GetComponent<MeshRenderer>();
        render.enabled = isvisible;
    }

}
