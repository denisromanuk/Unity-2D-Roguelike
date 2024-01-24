using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private GameObject MainCam;
    private Enemy _enemy;

    void Awake() 
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    
    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            //camera focused on room wheres player
            MainCam.transform.position = gameObject.transform.position;
        }
    }
}
