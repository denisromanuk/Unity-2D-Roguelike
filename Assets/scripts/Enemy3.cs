using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [Header("Enemy stats:")]
    public float hp; //default: 30
    public float dmg; //default: 1
    public float speed; //default: 0.03f

    private Enemy3Movement _movement;

    //use Start() instead of Awake() to dissable it in inspector
    void Start() {
        _movement = gameObject.AddComponent<Enemy3Movement>();
    }

    public void Stats(float h, float d, float s)
    {
        hp = h;
        dmg = d;
        speed = s;
    }

    public void GetDamage(float damageTaken)
    {
        hp -= damageTaken;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

class Enemy3Movement : MonoBehaviour
{
    Enemy3 _enemy3;
    GameObject _player;

    float time = 2f;
    
    void Awake() {
        _enemy3 = gameObject.GetComponent<Enemy3>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void Update() 
    {
        //disable RigidBody.Sleeping cause of OnTriggerStay2D() issues:
        _player.GetComponent<Rigidbody2D>().AddForce(Vector2.zero);
    }

    void MoveTowardsPlayer()
    {
        Vector2 enemyPos = gameObject.transform.position;
        gameObject.transform.position = Vector2.MoveTowards(enemyPos, _player.transform.position, _enemy3.speed);
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().GetDamage(1);
        }
    }

    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            time -= Time.deltaTime;
            
            if(time <= 0){
                collider.gameObject.GetComponent<Player>().GetDamage(1);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            time = 2f;
        }
    }
}
