using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    private Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindObjectOfType<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D colEnter) 
    {
        if(colEnter.CompareTag("Player"))
        {
            colEnter.gameObject.GetComponent<Player>().GetDamage(_enemy.dmg);
            Destroy(gameObject);
        }
        if(colEnter.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }
}
