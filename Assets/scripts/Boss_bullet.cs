using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_bullet : MonoBehaviour
{
    private Boss1 _boss1;

    // Start is called before the first frame update
    void Start()
    {
        _boss1 = FindObjectOfType<Boss1>();
    }

    void OnTriggerEnter2D(Collider2D colEnter) 
    {
        if(colEnter.CompareTag("Player"))
        {
            colEnter.gameObject.GetComponent<Player>().GetDamage(_boss1.dmg);
            Destroy(gameObject);
        }
        if(colEnter.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }
}
