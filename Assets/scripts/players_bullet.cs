using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Player _player;

    // Start is called before the first frame update
    void Awake()
    {
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D colEnter) 
    {
        if(colEnter.gameObject.tag == "Enemy")
        {
            if(colEnter.gameObject.GetComponent<Enemy1>()){
                if(colEnter.gameObject.GetComponent<Enemy1>().enabled){
                    colEnter.gameObject.GetComponent<Enemy1>().GetDamage(_player.dmg);
                }
            }
            if(colEnter.gameObject.GetComponent<Enemy3>()){
                if(colEnter.gameObject.GetComponent<Enemy3>().enabled){
                    colEnter.gameObject.GetComponent<Enemy3>().GetDamage(_player.dmg);
                }
            }
            Destroy(gameObject);
        }
        if(colEnter.gameObject.tag == "Boss")
        {
            if(colEnter.gameObject.GetComponent<Boss1>()){
                if(colEnter.gameObject.GetComponent<Boss1>().enabled){
                    colEnter.gameObject.GetComponent<Boss1>().GetDamage(_player.dmg);
                }
            }
            if(colEnter.gameObject.GetComponent<Boss2>()){
                if(colEnter.gameObject.GetComponent<Boss2>().enabled){
                    colEnter.gameObject.GetComponent<Boss2>().GetDamage(_player.dmg);
                }
            }
            Destroy(gameObject);
        }
        if(colEnter.CompareTag("TileMap"))
        {
            Destroy(gameObject);
        }
    }
}
