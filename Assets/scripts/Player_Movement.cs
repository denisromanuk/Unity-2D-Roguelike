using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public Player Player_script;
    private Vector2 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame - !note: get called diffent amount of times depending on ur fps
    void Update()
    {   
        float directionX = Input.GetAxisRaw("Horizontal"); //return horizontal direction (-1,1)
        float directionY = Input.GetAxisRaw("Vertical"); //return vertical direction (-1,1)
        
        moveDirection = new Vector2(directionX, directionY).normalized; //normalized returns this vector with a magnitude of 1 at max
    }

    // Fixed Update is called every fixed framerate frame => better 4 handling physics than regular update
    void FixedUpdate() {
        rb.velocity = new Vector2(moveDirection.x * Player_script.speed, moveDirection.y * Player_script.speed);
    }
}


