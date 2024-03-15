using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp; //default: 6, max: 12
    public float dmg; //default: 12, min: 0
    public float speed; //default: 4, min: 0, max: 10
    public float fireRate; //default: 0.85f, max: 0.3f
    private AudioManager _audiomanager;

    void Awake() {
        _audiomanager = FindAnyObjectByType<AudioManager>().GetComponent<AudioManager>();
    }

    public void Stats(float h, float d, float s, float fr)
    {
        hp = h;
        dmg = d;
        speed = s;
        fireRate = fr;
    }

    void FixedUpdate() {
        limitStats();

        if(hp <= 0){
            Destroy(gameObject);
        }
    }

    void limitStats(){
        //hp:
        if(hp > 12){hp = 12;}
        //dmg:
        if(dmg < 0){dmg = 0;}
        //speed:
        if(speed > 10){speed = 10;}
        if(speed < 0){speed = 0;}
        //firerate:
        if(fireRate < 0.3f){fireRate = 0.3f;}
    }

    public void GetDamage(float damageTaken)
    {
        hp -= damageTaken;
        _audiomanager.PlaySFX(_audiomanager.hit);
    }
}
