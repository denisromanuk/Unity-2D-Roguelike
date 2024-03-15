using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    Enemy1 _enemy;
    GameObject _player;

    public Rigidbody2D rb;
    private Vector2 moveDirection;
    byte moveState = 0;


    void Awake()
    {
        _enemy = GetComponent<Enemy1>();
        _player = GameObject.FindGameObjectWithTag("Player");
        charge();
    }
    
    void FixedUpdate()
    {
        move();

        switch(moveState){
            case 0:
                CancelInvoke("charge");
                Invoke("stopMoving", 1.2f);
                break;
            case 1:
                CancelInvoke("stopMoving");
                Invoke("charge", 0.8f);
                break;
        } 
    }

    void move()
    {
        rb.velocity = new Vector2(moveDirection.x * _enemy.speed, moveDirection.y * _enemy.speed);
    }

    void charge()
    {
        //vector towards players position:
        moveDirection = new Vector2(_player.transform.position.x - transform.position.x, _player.transform.position.y - transform.position.y).normalized;
        moveState = 0;
    }

    void stopMoving()
    {
        moveDirection = Vector2.zero;
        moveState = 1;
    }

    
    //player gets damage on collision with enemy
    void OnCollisionEnter2D(Collision2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().GetDamage(1);
        }
    }
        
    
}