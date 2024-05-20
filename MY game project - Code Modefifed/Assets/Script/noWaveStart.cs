using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noWaveStart : MonoBehaviour
{
    public bool inSpwan = false;
    public GameObject NextWaveText;
    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerTag"))
        {
            inSpwan = true;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("PlayerTag"))
        {
            inSpwan = false;
        }
    }

    void Update()
    {
        if(inSpwan == true)
        {
            NextWaveText.SetActive(false);
        }
    }
}
