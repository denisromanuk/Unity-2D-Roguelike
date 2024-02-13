using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyTrigger : MonoBehaviour
{
    public Tile door;
    private Tilemap roomtilemap;

    //positions for each door placement:
    private Vector3Int[] up = {new Vector3Int(-1, 5, 0), new Vector3Int(0, 5, 0)};
    private Vector3Int[] right = {new Vector3Int(10, 0, 0), new Vector3Int(10, -1, 0)};
    private Vector3Int[] down = {new Vector3Int(-1, -6, 0), new Vector3Int(0, -6, 0)};
    private Vector3Int[] left = {new Vector3Int(-11, 0, 0), new Vector3Int(-11, -1, 0)};

    private List<GameObject> _enemies = new List<GameObject>();
    private List<GameObject> _enemies2 = new List<GameObject>();

    private List<Collider2D> EnemiesInTrigger = new List<Collider2D>();

    void Awake() 
    {
        roomtilemap = gameObject.transform.parent.GetChild(0).GetComponent<Tilemap>();

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

    bool u = false;
    bool r = false;
    bool d = false;
    bool l = false;
    void Update() 
    {
        foreach (Collider2D enemy in EnemiesInTrigger)
        {
            if(enemy.IsDestroyed())
            {
                Debug.Log("DED");
                EnemiesInTrigger.Remove(enemy);
            }
        }
        Debug.Log(EnemiesInTrigger.Count);

        if(EnemiesInTrigger.Count <= 0)
        {
            u = false; r = false; d = false; l = false;
        }

        if(u){
            AddDoorTiles(up);
        }
        if(r){
            AddDoorTiles(right);
        }
        if(d){
            AddDoorTiles(down);
        }
        if(l){
            AddDoorTiles(left);
        }
    }

    void AddDoorTiles(Vector3Int[] tilesposition)
    {
        for (int i = 0; i < tilesposition.Length; i++)
        {
            roomtilemap.SetTile(tilesposition[i], door);
        }
    }

    private bool PlayerInTrigger = false;
    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            PlayerInTrigger = true;
            //bomber enemies starts cooldown:
            foreach (GameObject enemy2 in _enemies2)
            {
                if(enemy2 != null)
                {
                    enemy2.GetComponent<Enemy2>().enabled = true;
                }
            }
            
            //charger enemies starts shooting & moving:
            foreach (GameObject enemy in _enemies)
            {
                if(enemy != null)
                {
                    enemy.GetComponent<enemy_movement>().enabled = true;
                    enemy.GetComponent<Enemy_Script>().enabled = true;
                }
            }
        }
        if(PlayerInTrigger && collider.gameObject.tag == "Enemy")
        {
            if(!EnemiesInTrigger.Contains(collider))
            {
                EnemiesInTrigger.Add(collider);
            }
            
            if(roomtilemap.GetTile(up[0]) == null){
                u = true;
            }
            if(roomtilemap.GetTile(right[0]) == null){
                r = true;
            }
            if(roomtilemap.GetTile(down[0]) == null){
                d = true;
            }
            if(roomtilemap.GetTile(left[0]) == null){
                l = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            PlayerInTrigger = false;
        }
    }
}
