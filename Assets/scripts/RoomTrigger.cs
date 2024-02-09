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

    Vector2 playerdirection;

    private void Update() {
        
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            if(playerdirection == Vector2.up){ //up
                Debug.Log("UP");
                collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y + 4, collider.transform.position.z);
            }
            if(playerdirection == Vector2.down){ //down
                Debug.Log("DOWN");
                collider.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y - 4, collider.transform.position.z);
            }
            if(playerdirection == Vector2.right){ //right
                Debug.Log("RIGHT");
                collider.transform.position = new Vector3(collider.transform.position.x + 4, collider.transform.position.y, collider.transform.position.z);
            }
            if(playerdirection == Vector2.left){ //left
                Debug.Log("LEFT");
                collider.transform.position = new Vector3(collider.transform.position.x - 4, collider.transform.position.y, collider.transform.position.z);
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
