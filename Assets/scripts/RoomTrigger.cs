using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private float offsetX;
    private float offsetY;

    private float teleport_offset = 1.75f;
    private GameObject MainCam;

    void Awake() 
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {

            if(offsetY >= 5.5f)
            {   //UP
                collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y + teleport_offset, collider.transform.position.z);
            }
            if(offsetY <= -5.5f)
            {   //DOWN
                collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y - teleport_offset, collider.transform.position.z);
            }
            if(offsetX >= 10.2f)
            {   //RIGHT
                collider.transform.position = new Vector3(collider.transform.position.x + teleport_offset, collider.transform.position.y, collider.transform.position.z);
            }
            if(offsetX <= -10.2f)
            {   //LEFT
                collider.transform.position = new Vector3(collider.transform.position.x - teleport_offset, collider.transform.position.y, collider.transform.position.z);
            }
        }
    }
    
    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            offsetX = collider.transform.position.x - gameObject.transform.position.x;
            offsetY = collider.transform.position.y - gameObject.transform.position.y;

            //focus camera to a room where player is:
            MainCam.transform.position = gameObject.transform.position;
        }
    }
}
