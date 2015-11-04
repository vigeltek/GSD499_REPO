using UnityEngine;
using System.Collections;

public class UIMiniMap : MonoBehaviour {
    private Camera _miniMapCamera;
    private RenderTexture _miniMapTexture;

    private float mouseX;
    private float mouseY;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;

        Debug.Log("X: " + mouseX + " Y: " + mouseY);
    }
    void OnMouseEnter()
    {
        Debug.Log("Mouse Entered");
    }
    void OnMouseOver()
    {
        Debug.Log("Mouse Over");
    }
    void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
    }

    void Awake()
    {
        _miniMapCamera = GetComponent<Camera>();
        Debug.Log("Height: " +  _miniMapCamera.targetTexture.height);
        Debug.Log("Width: " + _miniMapCamera.targetTexture.width);
        Debug.Log("X: " + _miniMapCamera.transform.position.x);
        Debug.Log("Y: " + _miniMapCamera.transform.position.y);

    }
}
