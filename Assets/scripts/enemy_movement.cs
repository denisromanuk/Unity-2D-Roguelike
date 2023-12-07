using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    public Rigidbody2D rb;
    float time = 0f;
    float timeDelay = 1f;
    private Vector2 moveDirection;


    // Start is called before the first frame update
    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += 1f * Time.deltaTime;
        if(time > timeDelay)
        {
            time = 0f;
            moveDirection = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)).normalized;
            moveRandomly();
        }
        moveDirection = Vector2.zero;
    }

    void moveRandomly()
    {
        rb.velocity = new Vector2(moveDirection.x * 1, moveDirection.y * 1);
    }
}
