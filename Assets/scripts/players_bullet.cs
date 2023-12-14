using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Player _player;
    private Enemy _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -12f || transform.position.x > 12f)
        {
            Destroy(gameObject);
        }

        if(transform.position.y < -7f || transform.position.y > 7f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D colEnter) 
    {
        if(colEnter.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY");
            _enemy.GetDamage(_player.dmg);
            Destroy(gameObject);
        }
        if(colEnter.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }


}
