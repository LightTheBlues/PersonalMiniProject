using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    public string PlayerTag = "PlayerTag";

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PlayerTag);
        cam = player.transform.Find("Camera");
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
