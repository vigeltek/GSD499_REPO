using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    private Camera _mainCamera;
    public float ZoomAmount = 0;

    public float topLimit = 0;
    public float bottomLimit = 0;
    public float leftLimit = 0;
    public float rightLimit = 0;

    private float CurrentOrthoSize = 125;
    private float MaxToClamp = 245;
    private float MinToClamp = 75;
    private float ROTSpeed = 10;
    public float sensitivityX = 3.0f;
    public float sensitivityY = 3.0f;
    private float mouseX;
    private float mouseY;
    private Vector3 cameraPos;
    private bool bDragging = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("X: " + _mainCamera.transform.position.x + " Y: " + _mainCamera.transform.position.y + " Z: " + _mainCamera.transform.position.z);

        if (bDragging)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            cameraPos = new Vector3(-mouseX * sensitivityX, 0, -mouseY * sensitivityY);

            _mainCamera.transform.position += cameraPos;
            if (_mainCamera.transform.position.x > rightLimit)
            {
                _mainCamera.transform.position = new Vector3(rightLimit, _mainCamera.transform.position.y, _mainCamera.transform.position.z);
            }
            else if (_mainCamera.transform.position.x < leftLimit)
            {
                _mainCamera.transform.position = new Vector3(leftLimit, _mainCamera.transform.position.y, _mainCamera.transform.position.z);
            }

            if (_mainCamera.transform.position.z > topLimit)
            {
                _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, topLimit);
            }
            else if (_mainCamera.transform.position.z < bottomLimit)
            {
                _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, bottomLimit);
            }

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
        else if (Input.GetMouseButtonUp(0) && bDragging)
        {
            bDragging = false;
            //  Debug.Log("Drag End");
        }

    }

    void Awake()
    {
        // Debug.Log("Got Orthographic camera");
        _mainCamera = GetComponent<Camera>();
        _mainCamera.orthographicSize = CurrentOrthoSize;
    }
}
