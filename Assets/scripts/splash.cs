using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash : MonoBehaviour
{
    private Player _player;
    bool playerDamaged = false;

    void Start() 
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

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
                _player.GetDamage(3);
                playerDamaged = true;
            }
        }
    }
}
