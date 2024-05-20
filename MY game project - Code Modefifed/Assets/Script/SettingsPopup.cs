using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mouseCursor;
    public void Open()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mouseCursor.SetActive(false);
        Time.timeScale = 0;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseCursor.SetActive(true);
        Time.timeScale = 1;
    }


    void Start()
    {
       
    }

	

    // Update is called once per frame
    void Update()
    {
       
    }
}
