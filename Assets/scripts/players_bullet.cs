using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Player _player;

    // Start is called before the first frame update
    void Awake()
    {
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D colEnter) 
    {
        if(colEnter.gameObject.tag == "Enemy")
        {
            if(!colEnter.gameObject.GetComponent<Enemy2>())
            {
                colEnter.gameObject.GetComponent<Enemy>().GetDamage(_player.dmg);
            }
            Destroy(gameObject);
        }
        if(colEnter.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }
}
