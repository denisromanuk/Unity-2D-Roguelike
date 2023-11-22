using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float dmg;
    public float speed;
    public float fireRate;

    /*public Player(float h, float d, float s, float fr)
    {
        hp = h;
        dmg = d;
        speed = s;
        fireRate = fr;
    }*/

    public void Stats(float h, float d, float s, float fr)
    {
        hp = h;
        dmg = d;
        speed = s;
        fireRate = fr;
    }
}
