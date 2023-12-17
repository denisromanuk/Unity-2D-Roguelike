using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Player _player;
    private List<Enemy> _enemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
        {
			_enemies.Add(enemy.GetComponent<Enemy>());
		}
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
            //zjisti kterej enemy triggernul Trigger:
            foreach (Enemy enemy in _enemies)
            {
                if (colEnter.gameObject == enemy.gameObject)
                {
                    enemy.GetDamage(_player.dmg);
                    Destroy(gameObject);
                    break;
                }
            }
        }
        if(colEnter.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }


}
