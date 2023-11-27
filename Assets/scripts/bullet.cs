using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
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

    void OnCollisionEnter2D(Collision2D col) 
    {
        Debug.Log("LOL");
    }

    void DealDamage()
    {
        
    }


}
