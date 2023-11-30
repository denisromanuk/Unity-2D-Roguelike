using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Player _player;
    public Enemy _enemy1;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _enemy1 = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
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
        //ignoruje bullet prefab:
        if(!colEnter.CompareTag("Bullet"))
        {
            if(colEnter.CompareTag("Player"))
            {
                _player.GetDamage(1);
                Destroy(gameObject);
            }
            if(colEnter.CompareTag("Enemy"))
            {
                _enemy1.GetDamage(_player.dmg);
                Destroy(gameObject);
            }
        }
        else{
            Debug.Log("hittnuls střelu");
        }
        
    }


}
