using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp; //default: 6
    public float dmg; //default: 12
    public float speed; //default: 4
    public float fireRate; //default: 0.85

    public void Stats(float h, float d, float s, float fr)
    {
        hp = h;
        dmg = d;
        speed = s;
        fireRate = fr;
    }

    void FixedUpdate() {
        if(hp <= 0){
            Destroy(gameObject);
        }
    }

    public void GetDamage(float damageTaken)
    {
        hp -= damageTaken;
    }
}
