using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public int MaxRoomCount; //default: 7

    public GameObject RoomPrefab;

    private Vector3[] spawnpoints = {
        new Vector3(0, 12.2f, -9), //up
        new Vector3(21.5f, 0, -9), //right
        new Vector3(0, -12.2f, -9), //down
        new Vector3(-21.5f, 0, -9) //left
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
        Debug.Log($"max: {MaxRoomCount}");
    }

    void SpawnRooms(int minTimes, int maxTimes, GameObject caller)
    {
        int spawnedCount = 0;

        for(int i = 0; i < spawnpoints.Length; i++)
        {
            if(Random.Range(0,2) != 0) //50% chance to spawn
            {
                if(spawnedCount >= maxTimes || MaxRoomCount <= 0)
                {
                    return;
                }

                if(!currentRoomsPositions.Contains((Vector2)(caller.transform.position + spawnpoints[i])))
                {
                    InstantiateRoomPrefab(RoomPrefab, caller.transform.position + spawnpoints[i]);
                    spawnedCount++;
                }
            }
        }
        if(spawnedCount < minTimes)
        {
            Vector3 rng = caller.transform.position + spawnpoints[Random.Range(0,4)];
            if(!currentRoomsPositions.Contains((Vector2)rng))
            {
                InstantiateRoomPrefab(RoomPrefab, rng);
                spawnedCount++;
            }
        }
        if(spawnedCount < maxTimes && MaxRoomCount > 0)
        {
            Vector3 rng = caller.transform.position + spawnpoints[Random.Range(0,4)];
            if(!currentRoomsPositions.Contains((Vector2)rng))
            {
                InstantiateRoomPrefab(RoomPrefab, rng);
                spawnedCount++;
            }
        }

        
    }

    void AddRemaining()
    {
        if(MaxRoomCount > 0)
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
        currentRoomsPositions.Add((Vector2)(position));
        MaxRoomCount--;
    }
}
