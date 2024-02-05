using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _enemies2 = new List<GameObject>();

    void Awake() 
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
        {
            if(enemy.GetComponent<Enemy>())
            {
                if(enemy.transform.IsChildOf(gameObject.transform))
                {
                    _enemies.Add(enemy);
                }
            }

            if(enemy.GetComponent<Enemy2>())
            {
                //if enemy is child of gameobject(= this room):
                if(enemy.transform.IsChildOf(gameObject.transform))
                {
                    _enemies2.Add(enemy);
                }
            }
        }
    }

    void Update() 
    {
        //Debug.Log(_enemies2.Count);
    }

    
    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            //bomber enemies starts cooldown:
            foreach (GameObject enemy2 in _enemies2)
            {
                enemy2.GetComponent<Enemy2>().startCountdown = true;
            }
            
            //charger enemies starts shooting & moving:
            foreach (GameObject enemy in _enemies)
            {
                enemy.GetComponent<enemy_movement>().startMoving = true;
                enemy.GetComponent<enemy_movement>().Setup(collider);

                enemy.GetComponent<Enemy_Script>().startShooting = true;
                enemy.GetComponent<Enemy_Script>().setup = true;
            }
        }
    }
}
