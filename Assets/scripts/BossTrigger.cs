using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossTrigger : MonoBehaviour
{
    public Tile door;
    public GameObject trapdoor;
    private Tilemap roomtilemap;
    private AudioManager _audiomanager;

    //positions for each door placement:
    private Vector3Int[] up = {new Vector3Int(-1, 5, 0), new Vector3Int(0, 5, 0)};
    private Vector3Int[] right = {new Vector3Int(10, 0, 0), new Vector3Int(10, -1, 0)};
    private Vector3Int[] down = {new Vector3Int(-1, -6, 0), new Vector3Int(0, -6, 0)};
    private Vector3Int[] left = {new Vector3Int(-11, 0, 0), new Vector3Int(-11, -1, 0)};

    private List<GameObject> _bosses = new List<GameObject>();

    private List<Collider2D> BossesInTrigger = new List<Collider2D>();

    void Awake() 
    {
        _audiomanager = FindAnyObjectByType<AudioManager>().GetComponent<AudioManager>();
        roomtilemap = gameObject.transform.parent.GetChild(0).GetComponent<Tilemap>();

        foreach(GameObject boss in GameObject.FindGameObjectsWithTag("Boss")) 
        {
            if(boss.GetComponent<Boss1>() || boss.GetComponent<Boss2>())
            {
                if(boss.transform.IsChildOf(gameObject.transform))
                {
                    _bosses.Add(boss);
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
        foreach (Collider2D boss in BossesInTrigger)
        {
            if(boss.IsDestroyed())
            {
                BossesInTrigger.Remove(boss);
            }
        }
        //Debug.Log(BossesInTrigger.Count);

        if(BossesInTrigger.Count <= 0)
        {
            trapdoor.GetComponent<NextStage>().bossdead = true;
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
    bool playedBoss = false;

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player" && !playedBoss)
        {
            _audiomanager._musicSource.clip = _audiomanager.boss;
            _audiomanager._musicSource.time = 12.5f;
            _audiomanager._musicSource.Play();
            playedBoss = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            PlayerInTrigger = true;

            //charger enemies starts shooting & moving:
            foreach (GameObject boss in _bosses)
            {
                if(boss != null)
                {
                    if(boss.GetComponent<Boss1>()){
                        boss.GetComponent<Boss1>().enabled = true;
                    }
                    if(boss.GetComponent<Boss2>()){
                        boss.GetComponent<Boss2>().enabled = true;
                    }
                }
            }
        }
        
        if(PlayerInTrigger && collider.gameObject.tag == "Boss")
        {
            trapdoor.GetComponent<NextStage>().bossdead = false;

            
            if(!BossesInTrigger.Contains(collider))
            {
                BossesInTrigger.Add(collider);
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
            if(_audiomanager._musicSource.clip != _audiomanager.level){
                _audiomanager._musicSource.volume = 1f;
                _audiomanager._musicSource.clip = _audiomanager.level;
                _audiomanager._musicSource.loop = true;
                _audiomanager._musicSource.Play();
            }
        }
    }
}
