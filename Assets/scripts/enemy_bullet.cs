using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    private Enemy1 _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = FindObjectOfType<Enemy1>();
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
