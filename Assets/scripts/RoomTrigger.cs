using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private GameObject MainCam;

    void Awake() 
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            //focus camera to a room where player is:
            MainCam.transform.position = gameObject.transform.position;
        }
    }
}
