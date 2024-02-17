using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float hpUP;
    public float dmgUP;
    public float speedUP;
    public float fireRateUP;

    public void AddStats(Player _player)
    {
        _player.hp += hpUP;
        _player.dmg += dmgUP;
        _player.speed += speedUP;
        _player.fireRate += fireRateUP;
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            AddStats(collider.GetComponent<Player>());
            Destroy(gameObject);
        }
    }
}
