using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
