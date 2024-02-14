using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public int MinRequired; //default: 3
    public int MaxRequired; //default: 5
    private int RequiredRoomCount; //default: 7

    public GameObject RoomPrefab;

    public List<GameObject> EnemyRoomPresets = new List<GameObject>();

    private Vector2[] spawnpoints = {
        new Vector2(0, 12.2f), //up
        new Vector2(21.5f, 0), //right
        new Vector2(0, -12.2f), //down
        new Vector2(-21.5f, 0) //left
    };

    private List<Vector2> currentRoomsPositions = new List<Vector2>();

    void Start()
    {
        RequiredRoomCount = Random.Range(MinRequired, MaxRequired + 1);
        currentRoomsPositions.Add(new Vector2(0, 0)); //StartRoom position

        SpawnRooms();
    }

    void Update() 
    {
        //Debug.Log($"max: {RequiredRoomCount}");
    }

    void SpawnRooms()
    {
        //startroom spawns 1-4 rooms:
        foreach(GameObject startroom in GameObject.FindGameObjectsWithTag("StartRoom"))
        {
            int startroomsCount = Random.Range(1,5); //1-4

            if(startroomsCount >= RequiredRoomCount){
                startroomsCount = RequiredRoomCount;
            }

            for (int i = 0; i < startroomsCount; i++)
            {
                if(IsPositionAlreadyInList((Vector2)startroom.transform.position + spawnpoints[i])){
                    continue;
                }
                InstantiateRoomPrefab(RoomPrefab, (Vector2)startroom.transform.position + spawnpoints[i]);
            }
        }

        while(RequiredRoomCount > 0)
        {
            foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room"))
            {
                int roomsCount = Random.Range(0,5); //0-4

                for (int i = 0; i < roomsCount; i++)
                {
                    if(RequiredRoomCount <= 0){
                        break;
                    }

                    if(IsPositionAlreadyInList((Vector2)room.transform.position + spawnpoints[i])){
                        continue;
                    }
                    InstantiateRoomPrefab(RoomPrefab, (Vector2)room.transform.position + spawnpoints[i]);
                }
            }
        }
    }

    bool IsPositionAlreadyInList(Vector2 position)
    {
        for (int i = 0; i < currentRoomsPositions.Count; i++)
        {
            if (currentRoomsPositions[i] == position){
                return true;
            }
        }
        return false;
    }

    void InstantiateRoomPrefab(GameObject prefab, Vector3 position)
    {
        GameObject room = Instantiate(prefab, (Vector2)position, Quaternion.identity);
        currentRoomsPositions.Add((Vector2)position);
        RequiredRoomCount--;

        GeneratePresets(room);
    }

    void GeneratePresets(GameObject enemyroom)
    {
        GameObject rngPreset = EnemyRoomPresets[Random.Range(0, EnemyRoomPresets.Count)];
        Instantiate(rngPreset, (Vector2)enemyroom.transform.position, Quaternion.identity, enemyroom.transform);
    }
}
