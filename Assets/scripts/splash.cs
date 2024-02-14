using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash : MonoBehaviour
{
    bool playerDamaged = false;

    void FixedUpdate() 
    {
        Invoke("Clear", 0.66f);
    }

    void Clear()
    {
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D colEnter) 
    {
        if(colEnter.CompareTag("Player"))
        {
            if(!playerDamaged)
            {
                //damage player:
                colEnter.gameObject.GetComponent<Player>().GetDamage(3);
                playerDamaged = true;
            }
        }
    }
}
