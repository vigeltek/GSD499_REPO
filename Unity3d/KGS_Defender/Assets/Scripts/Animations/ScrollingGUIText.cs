using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollingGUIText : MonoBehaviour
{

    /* Public Variables    */

    // The array of GUIText elements to display and scroll
    public GameObject[] textElements;
    int size;
    private List<float> yPos = new List<float>();
    // The delay time before displaying the GUIText elements
    public float displayTime = 5.0f;

    // The delay time before starting the GUIText scroll
    public float scrollTime = 5.0f;

    // The scrolling speed
    public float scrollSpeed = 0.2f;
    private bool bInit = false;
    void Awake()
    {
        int index = 0;
        size = textElements.Length;
        for (int i = 0; i < textElements.Length; i++)
        {
            GameObject curr = textElements[i];
           // float y = curr.transform.position.y;
           // yPos.Add(y);
        }

        bInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bInit)
            return;

        // start display count down
        displayTime -= Time.deltaTime;

        if (displayTime < 0)
        {
            // if it is time to display, start the scrolling count down timer
            scrollTime -= Time.deltaTime;
        }

        // if it is time to scroll, cycle through the GUIElements and
        // increase their Y position by the desired speed
        if (scrollTime < 0)
        {
            for (int i = 0; i < textElements.Length; i++)
            {
                GameObject current = textElements[i];
                //text.transform.Translate(Vector3.up * scrollSpeed);
                float x = current.transform.position.x;
                float z = current.transform.position.z;
                yPos[i] = current.transform.position.y + scrollSpeed;

                current.transform.position = Vector3.Lerp(current.transform.position, new Vector3(x, yPos[i], z), 1.0f);
            }
        }
    }
}
