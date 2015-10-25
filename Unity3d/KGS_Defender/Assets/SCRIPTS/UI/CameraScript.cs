using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    private Camera _mainCamera;
    public float ZoomAmount = 0;

    public float topLimit = 0;
    public float bottomLimit = 0;
    public float leftLimit = 0;
    public float rightLimit = 0;
    public int movementMargin = 35;
    private float maxZoom = 35;
    private float minZoom = 3;
    private float ROTSpeed = 30;
    public float sensitivityX = 3.0f;
    public float sensitivityY = 3.0f;

    private bool bRotating = false;
    private float xRot = 0;
    private float yRot = 0;
    private float yaw = 0;
    private float pitch = 0;
    //private float mouseX;
    //private float mouseY;
    private Vector3 cameraPos;
    private bool bDragging = false;
    private Quaternion origRotation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int scrWidth = Screen.width;
        int scrHeight = Screen.height;
        float newX = 0;
        float newY = 0;
        float newZ = 0;

        //Debug.Log("X: " + _mainCamera.transform.position.x + " Y: " + _mainCamera.transform.position.y + " Z: " + _mainCamera.transform.position.z);
        /*
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;

        Debug.Log("X: " + mouseX + " Y: " + mouseY);
        */
        if (Input.GetMouseButtonDown(2))
        {
            // zero camera
            _mainCamera.transform.rotation = origRotation;
            yaw = 0;
            pitch = 0;
        }

        if (Input.GetMouseButton(1))
        {
            // rotate camera enabled
            //Debug.Log("Rotating ");
            if (!bRotating)
            {
                bRotating = true;
                xRot = Input.mousePosition.x;
                yRot = Input.mousePosition.y;
            }
        }
        else
        {
            // rotate camera disabled
            if (bRotating)
                bRotating = false;
        }

        if (bRotating)
        {
            if (Input.mousePosition.x > xRot + 5)
            {
                yaw += .25f;
                if (yaw > 22.5f)
                    yaw = 22.5f;
                else
                    _mainCamera.transform.Rotate(Vector3.up, .25f);

            }else if(Input.mousePosition.x < xRot - 5)
            {

                yaw -= .25f;
                if (yaw < -22.5f)
                    yaw = -22.5f;
                else
                    _mainCamera.transform.Rotate(Vector3.up, -.25f);
            }


            if (Input.mousePosition.y > yRot + 5)
            {
                pitch += .25f;
                if (pitch >= 22.5f)
                    pitch = 22.5f;
                else
                    _mainCamera.transform.Rotate(Vector3.left, .25f);
            }
            else if (Input.mousePosition.y < yRot - 5)
            {

                pitch -= .25f;
                if (pitch < -22.5f)
                    pitch = -22.5f;
                else
                    _mainCamera.transform.Rotate(Vector3.left, -.25f);
            }

        }
        else
        {

            /* WASD Key Control */
            if (Input.GetKey(KeyCode.W))
            {
                newY += sensitivityY;
            }

            if (Input.GetKey(KeyCode.A))
            {
                newX -= sensitivityX;
            }

            if (Input.GetKey(KeyCode.S))
            {
                newY -= sensitivityY;
            }

            if (Input.GetKey(KeyCode.D))
            {
                newX += sensitivityX;
            }

            /*
            if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= movementMargin)
            {
                newX -= sensitivityX;
            }
            else if (Input.mousePosition.x >= scrWidth - movementMargin)
            {
                newX += sensitivityX;
            }

            if (Input.mousePosition.y >= 0 && Input.mousePosition.y <= movementMargin)
            {
                newY -= sensitivityY;
            }
            else if (Input.mousePosition.y >= scrHeight - movementMargin)
            {
                newY += sensitivityY;
            }
            */
            cameraPos = new Vector3(newX, 0, newY);

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

        _mainCamera.fieldOfView -= (Input.GetAxis("Mouse ScrollWheel")*12);
        
        //Debug.Log("Zoom change: " + _mainCamera.fieldOfView);
        if (_mainCamera.fieldOfView <= 8.0f)
            _mainCamera.fieldOfView = 8.0f;
        else if (_mainCamera.fieldOfView >= 40.0f)
            _mainCamera.fieldOfView = 40.0f;

    }

    void Awake()
    {
        // Debug.Log("Got Orthographic camera");
        _mainCamera = GetComponent<Camera>();
        //_mainCamera.orthographicSize = 26;
        origRotation = _mainCamera.transform.rotation;
    }
}
