using UnityEngine;
using System.Collections;

public class TempScript : MonoBehaviour {
    

    private Camera _mainCamera;
    public float ZoomAmount = 0;
    public float CurrentOrthoSize = 125;
    public float MaxToClamp = 245;
    public float MinToClamp = 75;
    private float ROTSpeed = 10;
    public float sensitivityX = 3.0f;
    public float sensitivityY = 3.0f;
    private float mouseX;
    private float mouseY;
    public Vector3 cameraPos;
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

        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
       // Debug.Log("Zoom change: " + ZoomAmount);

        CurrentOrthoSize += ZoomAmount;
        if (CurrentOrthoSize < MinToClamp)
            CurrentOrthoSize = MinToClamp;
        if (CurrentOrthoSize > MaxToClamp)
            CurrentOrthoSize = MaxToClamp;

        _mainCamera.orthographicSize = CurrentOrthoSize;
        if (Input.GetMouseButtonDown(2))
        {
            ZoomAmount = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            bDragging = true;
           // Debug.Log("Drag Start");
        }
        else if(Input.GetMouseButtonUp(0) && bDragging)
        {
            bDragging = false;
           // Debug.Log("Drag End");
        }
       
	}

    void Awake()
    {
        //Debug.Log("Got Orthographic camera");
        _mainCamera = GetComponent<Camera>();
        _mainCamera.orthographicSize = CurrentOrthoSize;
    }
}

