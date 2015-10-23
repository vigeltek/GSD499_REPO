using UnityEngine;
using System.Collections;

public class placementPanel : MonoBehaviour {

    public GameObject currPrefab;

    public TPManager tpm;

    public MeshRenderer rend;

    public bool placementMode = true;

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
        if(tpm.sellMode)
        {
            if (currPrefab == null)
            {
                rend.material.SetColor("_Color", tpm.invalidColor);
            }
            else
            {
                rend.material.SetColor("_Color", tpm.highlightColor);
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
            if (tpm.canAfford())
            {
                currPrefab = (GameObject)Instantiate(tpm.GetTurret(), transform.position, transform.rotation);
                currPrefab.transform.parent = this.gameObject.transform;
                TPManager.PlacementMode();
            }
        }
        if(tpm.sellMode && currPrefab != null)
        {
            WeaponController wc = currPrefab.GetComponent<WeaponController>();
            int resource = wc.GetTurretType();
            Destroy(currPrefab);
            currPrefab = null;
            tpm.SellTurret(resource);
            tpm.SellMode();
        }

    }

    public void GridVisibility(bool isVisible)
    {
            MeshRenderer render = GetComponent<MeshRenderer>();
            render.enabled = isVisible;
    }

}
