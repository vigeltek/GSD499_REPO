using UnityEngine;
using System.Collections;

public class TempScript : MonoBehaviour {
    

    private Camera _mainCamera;
    private float sensitivityX = 3.0f;
    private float sensitivityY = 3.0f;
    private float mouseX;
    private float mouseY;
    private Vector3 cameraPos;
    private bool bDragging = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (bDragging)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            cameraPos = new Vector3(-mouseX * sensitivityX, 0, -mouseY * sensitivityY);

            _mainCamera.transform.position += cameraPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            bDragging = true;
            Debug.Log("Drag Start");
        }
        else if(Input.GetMouseButtonUp(0) && bDragging)
        {
            bDragging = false;
            Debug.Log("Drag End");
        }
       
	}

    void Awake()
    {
        Debug.Log("Got Orthographic camera");
        _mainCamera = GetComponent<Camera>();
    }
}

