using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomEdgeTrigger : MonoBehaviour
{
    public List<BoxCollider2D> triggers = new List<BoxCollider2D>();

    public Tile wall;
    public Tilemap roomtilemap;

    private Vector3Int[] up = {new Vector3Int(-1, 5, 0), new Vector3Int(0, 5, 0)};
    private Vector3Int[] right = {new Vector3Int(10, 0, 0), new Vector3Int(10, -1, 0)};
    private Vector3Int[] down = {new Vector3Int(-1, -6, 0), new Vector3Int(0, -6, 0)};
    private Vector3Int[] left = {new Vector3Int(-11, 0, 0), new Vector3Int(-11, -1, 0)};

    /*
    void Update() 
    {
        foreach(BoxCollider2D trigger in triggers)
        {
            if(!TriggerCollidesWithRoom(trigger))
            {
                //5,6
            }
        }  
    }

    bool TriggerCollidesWithRoom(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Room" || collider.gameObject.tag == "StartRoom")
        {
            return true;
        }
        return false;
    }*/

    void Awake() {
        PaintWallTiles(left);
        //PaintWallTiles(right);
        //PaintWallTiles(up);
        //PaintWallTiles(down);
    }

    void PaintWallTiles(Vector3Int[] tilesposition){
        for (int i = 0; i < tilesposition.Length; i++)
        {
            roomtilemap.SetTile(tilesposition[i], wall);
        }
        
    }
}
