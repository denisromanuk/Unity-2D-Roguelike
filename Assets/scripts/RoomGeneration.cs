using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public int MinRequired; //default: 5
    public int MaxRequired; //default: 7
    private int RequiredRoomCount;

    public GameObject EnemyRoomPrefab;
    public GameObject TreasureRoomPrefab;
    public GameObject BossRoomPrefab;

    public List<GameObject> EnemyRoomPresets = new List<GameObject>();
    public List<GameObject> TreasureRoomPresets = new List<GameObject>();
    public List<GameObject> BossRoomPresets = new List<GameObject>();

    private Vector2[] spawnpoints = {
        new Vector2(0, 12.2f), //up
        new Vector2(21.5f, 0), //right
        new Vector2(0, -12.2f), //down
        new Vector2(-21.5f, 0) //left
    };

    private Vector2 StartRoomPosition;
    private List<Vector2> EnemyRoomPositions = new List<Vector2>();
    private Vector2 TreasureRoomPosition;
    private Vector2 BossRoomPosition;
    List<Vector2> currentRoomsPositions = new List<Vector2>();
    float maxX = 0f;
    float maxY = 0f;

    void Start()
    {
        RequiredRoomCount = Random.Range(MinRequired, MaxRequired + 1);
        StartRoomPosition = new Vector2(0, 0); //StartRoom position

        SpawnRooms();
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
                int rng = Random.Range(0, spawnpoints.Length);

                if(IsPositionAlreadyInList((Vector2)startroom.transform.position + spawnpoints[rng])){
                    continue;
                }
                InstantiateRoomPrefab(EnemyRoomPrefab, (Vector2)startroom.transform.position + spawnpoints[rng]);
            }
        }
        
        while(RequiredRoomCount > 0)
        {
            foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room"))
            {
                int roomsCount = Random.Range(0,5); //0-4

                for (int i = 0; i < roomsCount; i++)
                {
                    int rng = Random.Range(0, spawnpoints.Length);

                    if(RequiredRoomCount <= 0){
                        break;
                    }

                    if(IsPositionAlreadyInList((Vector2)room.transform.position + spawnpoints[rng])){
                        continue;
                    }
                    InstantiateRoomPrefab(EnemyRoomPrefab, (Vector2)room.transform.position + spawnpoints[rng]);
                }
            }
        }
        GenerateSpecial();
    }

    void GenerateSpecial()
    {
        currentRoomsPositions.Add(StartRoomPosition);

        for (int i = 0; i < EnemyRoomPositions.Count; i++)
        {
            currentRoomsPositions.Add(EnemyRoomPositions[i]);
        }

        for (int i = 0; i < currentRoomsPositions.Count; i++)
        {
            if(System.Math.Abs(currentRoomsPositions[i].x) > maxX){
                maxX = currentRoomsPositions[i].x;
                BossRoomPosition = currentRoomsPositions[i];
            }
            if(System.Math.Abs(currentRoomsPositions[i].y) > maxY){
                maxY = currentRoomsPositions[i].y;
                BossRoomPosition = currentRoomsPositions[i];
            }
        }
        //Debug.Log($"BOSS: {maxX};{maxY}");
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            if((Vector2)room.transform.position == BossRoomPosition)
            {
                Destroy(room);
                EnemyRoomPositions.Remove((Vector2)room.transform.position);
                break;
            }
        }
        InstantiateRoomPrefab(BossRoomPrefab, BossRoomPosition);
        //currentRoomsPositions. += BossRoomPosition;

        //Debug.Log(enemyrooms.Length);
        TreasureRoomPosition = EnemyRoomPositions[Random.Range(0,EnemyRoomPositions.Count)];
        
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            if((Vector2)room.transform.position == TreasureRoomPosition)
            {
                Destroy(room);
                EnemyRoomPositions.Remove((Vector2)room.transform.position);
                break;
            }
        }
        InstantiateRoomPrefab(TreasureRoomPrefab, TreasureRoomPosition);
        //currentRoomsPositions.Add(treasureRoomPos);
        //Debug.Log(currentRoomsPositions.Count);
    }

    bool IsPositionAlreadyInList(Vector2 position)
    {
        if(position == StartRoomPosition){
            return true;
        }
        if(position == TreasureRoomPosition){
            return true;
        }
        if(position == BossRoomPosition){
            return true;
        }
        for (int i = 0; i < EnemyRoomPositions.Count; i++)
        {
            if (EnemyRoomPositions[i] == position){
                return true;
            }
        }
        return false;
    }

    void InstantiateRoomPrefab(GameObject prefab, Vector3 position)
    {
        GameObject room;

        switch (prefab.name)
        {
            case "Room":
                room = Instantiate(prefab, (Vector2)position, Quaternion.identity);
                EnemyRoomPositions.Add((Vector2)position);
                RequiredRoomCount--;

                GeneratePresets(room, "Room");
                break;
            case "TreasureRoom":
                room = Instantiate(prefab, (Vector2)position, Quaternion.identity);
                TreasureRoomPosition = (Vector2)position;
                RequiredRoomCount--;

                GeneratePresets(room, "TreasureRoom");
                break;
            case "BossRoom":
                room = Instantiate(prefab, (Vector2)position, Quaternion.identity);
                BossRoomPosition = (Vector2)position;
                RequiredRoomCount--;

                GeneratePresets(room, "BossRoom");
                break;
        }
    }

    void GeneratePresets(GameObject room, string prefabname)
    {
        GameObject rngPreset;

        switch(prefabname)
        {
            case "Room":
                rngPreset = EnemyRoomPresets[Random.Range(0, EnemyRoomPresets.Count)];
                Instantiate(rngPreset, (Vector2)room.transform.position, Quaternion.identity, room.transform);
                break;
            case "TreasureRoom":
                rngPreset = TreasureRoomPresets[Random.Range(0, TreasureRoomPresets.Count)];
                Instantiate(rngPreset, (Vector2)room.transform.position, Quaternion.identity, room.transform);
                break;
            case "BossRoom":
                rngPreset = BossRoomPresets[Random.Range(0, BossRoomPresets.Count)];
                Instantiate(rngPreset, (Vector2)room.transform.position, Quaternion.identity, room.transform);
                break;
        }
    }
}
