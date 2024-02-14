using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private float teleport_offset = 1.75f;
    private GameObject MainCam;

    void Awake() 
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    Vector2 playerdirection;

    private void Update() {
        
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            if(playerdirection == Vector2.up){ //up
                collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y + teleport_offset, collider.transform.position.z);
            }
            if(playerdirection == Vector2.down){ //down
                collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y - teleport_offset, collider.transform.position.z);
            }
            if(playerdirection == Vector2.right){ //right
                collider.transform.position = new Vector3(collider.transform.position.x + teleport_offset, collider.transform.position.y, collider.transform.position.z);
            }
            if(playerdirection == Vector2.left){ //left
                collider.transform.position = new Vector3(collider.transform.position.x - teleport_offset, collider.transform.position.y, collider.transform.position.z);
            }
        }
    }
    
    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            //focus camera to a room where player is:
            MainCam.transform.position = gameObject.transform.position;

            //players last moving direction:
            if(collider.GetComponent<Player_Movement>().moveDirection != Vector2.zero){
                playerdirection = collider.GetComponent<Player_Movement>().moveDirection;
            }
        }
    }
}
