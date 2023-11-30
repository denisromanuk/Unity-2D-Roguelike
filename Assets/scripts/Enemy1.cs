using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp; //default: 40
    public float dmg; //default: 1
    public float speed;
    public float fireRate; //default: 0.9

    public void Stats(float h, float d, float s, float fr)
    {
        hp = h;
        dmg = d;
        speed = s;
        fireRate = fr;
    }

    public void GetDamage(float damageTaken)
    {
        hp -= damageTaken;
        if(hp <= 0)
        {
            Debug.Log("enemy1 umřel");
            Destroy(gameObject);
        }
    }
}
