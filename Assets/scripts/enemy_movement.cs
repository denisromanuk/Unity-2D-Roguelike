using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    Enemy _enemy;
    Player _player;

    public Rigidbody2D rb;
    private Vector2 moveDirection;

    //timers in seconds
    float time = 0f;
    float time2 = 0f;
    float timeDelay = 1f; 

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //vector towards players position:
        moveDirection = new Vector2(_player.transform.position.x - transform.position.x, _player.transform.position.y - transform.position.y).normalized;
    }

    void FixedUpdate()
    {
        move();
        time += 1f * Time.deltaTime;
        if(time > timeDelay)
        {
            moveDirection = Vector2.zero;
            time2 += 1f * Time.deltaTime;
            if(time2 > timeDelay)
            {
                time = 0f;
                time2 = 0f;
                //vector towards players position:
                moveDirection = new Vector2(_player.transform.position.x - transform.position.x, _player.transform.position.y - transform.position.y).normalized;
            }
        }
    }

    void move()
    {
        rb.velocity = new Vector2(moveDirection.x * _enemy.speed, moveDirection.y * _enemy.speed);
    }

    //player gets damage on collision with enemy
    void OnCollisionEnter2D(Collision2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            _player.GetDamage(1);
        }
    }
        
    
}
