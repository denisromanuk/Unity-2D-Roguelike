using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public int RequiredRoomCount; //default: 7

    public GameObject RoomPrefab;

    private Vector2[] spawnpoints = {
        new Vector2(0, 12.2f), //up
        new Vector2(21.5f, 0), //right
        new Vector2(0, -12.2f), //down
        new Vector2(-21.5f, 0) //left
    };

    private List<Vector2> currentRoomsPositions = new List<Vector2>();

    void Start()
    {
        currentRoomsPositions.Add(new Vector2(0, 0)); //StartRoom position

        SpawnRooms(1, 4, gameObject);

        foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room")) 
        {
            SpawnRooms(0, 3, room);
		}
    }

    void Update() 
    {
        AddRemaining();
        //Debug.Log($"max: {RequiredRoomCount}");
    }

    void SpawnRooms(int minTimes, int maxTimes, GameObject caller)
    {
        int spawnedCount = 0;

        for(int i = 0; i < spawnpoints.Length; i++)
        {
            if(Random.Range(0,2) != 0) //50% chance to spawn
            {
                if(spawnedCount >= maxTimes || RequiredRoomCount <= 0)
                {
                    return;
                }

                if(!currentRoomsPositions.Contains((Vector2)caller.transform.position + spawnpoints[i]))
                {
                    InstantiateRoomPrefab(RoomPrefab, (Vector2)caller.transform.position + spawnpoints[i]);
                    spawnedCount++;
                }
            }
        }
        if(spawnedCount < minTimes)
        {
            Vector3 rng = (Vector2)caller.transform.position + spawnpoints[Random.Range(0,4)];
            if(!currentRoomsPositions.Contains((Vector2)rng))
            {
                InstantiateRoomPrefab(RoomPrefab, rng);
                spawnedCount++;
            }
        }
        
    }

    void AddRemaining()
    {
        if(RequiredRoomCount > 0)
        {
            foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room")) 
            {
                SpawnRooms(0, 3, room);
            }
        }
    }

    void InstantiateRoomPrefab(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
        currentRoomsPositions.Add((Vector2)position);
        RequiredRoomCount--;
    }
}
