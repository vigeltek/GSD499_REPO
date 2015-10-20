using UnityEngine;
using System.Collections;

public class UIGameOverWin : MonoBehaviour {

    private GameObject thisObj;
    private static UIGameOverWin instance;

    // Use this for initialization
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1;
        GameController.LoadMainMenu();
    }

    void Awake()
    {
        instance = this;
        thisObj = gameObject;

        transform.localPosition = Vector3.zero;

    }

    public static bool isOn = true;
    public static void Show() { instance._Show(); }
    public void _Show()
    {
        isOn = true;
        thisObj.SetActive(isOn);
    }
    public static void Hide() { instance._Hide(); }
    public void _Hide()
    {
        isOn = false;
        thisObj.SetActive(isOn);
    }
}
