using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    private Player _player;
    private Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _enemy = GameObject.FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D colEnter) 
    {
        if(colEnter.CompareTag("Player"))
        {
            _player.GetDamage(_enemy.dmg);
            Destroy(gameObject);
        }
        if(colEnter.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }
}
