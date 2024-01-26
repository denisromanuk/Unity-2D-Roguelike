using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetGeneration : MonoBehaviour
{
    
    public List<GameObject> EnemyRoomPresets = new List<GameObject>();

    // Update is called once per frame
    public void GeneratePresets(GameObject enemyroom)
    {
        GameObject rngPreset = EnemyRoomPresets[Random.Range(0, EnemyRoomPresets.Count)];
        Instantiate(rngPreset, (Vector2)enemyroom.transform.position, Quaternion.identity, enemyroom.transform);
    }
}
