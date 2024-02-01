using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomEdgeTrigger : MonoBehaviour
{
    public Tile wall;
    public Tilemap roomtilemap;

    private Vector3Int[] up = {new Vector3Int(-1, 5, 0), new Vector3Int(0, 5, 0)};
    private Vector3Int[] right = {new Vector3Int(10, 0, 0), new Vector3Int(10, -1, 0)};
    private Vector3Int[] down = {new Vector3Int(-1, -6, 0), new Vector3Int(0, -6, 0)};
    private Vector3Int[] left = {new Vector3Int(-11, 0, 0), new Vector3Int(-11, -1, 0)};

    List<GameObject> collidedRooms = new List<GameObject>();

    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "TileMap")
        {
            if(!collidedRooms.Contains(collider.gameObject))
            {
                collidedRooms.Add(collider.gameObject);
            }
        }

        for (int i = 0; i < collidedRooms.Count; i++)
        {
            Vector2 offset = (Vector2)gameObject.transform.position - (Vector2)collidedRooms[i].transform.position;
            Debug.Log($"{(Vector2)gameObject.transform.position} - {(Vector2)collidedRooms[i].transform.position} = {offset}");
            Debug.Log(collidedRooms.Count);

            
            if(offset == new Vector2(0, 12.2f)){ //up
                RemoveWallTiles(down);
            }
            if(offset == new Vector2(21.5f, 0)){ //right
                RemoveWallTiles(left);
            }
            if(offset == new Vector2(0, -12.2f)){ //down
                RemoveWallTiles(up);
            }
            if(offset == new Vector2(-21.5f, 0)){ //left
                RemoveWallTiles(right);
            }
        }
    }

    void RemoveWallTiles(Vector3Int[] tilesposition)
    {
        for (int i = 0; i < tilesposition.Length; i++)
        {
            roomtilemap.SetTile(tilesposition[i], null);
        }
    }
}
