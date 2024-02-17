using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public Transform ItemSpawner;
    public List<GameObject> ItemPrefabs = new List<GameObject>();

    private void Awake() {
        spawnitem();
    }

    public void spawnitem()
    {
        GameObject item = ItemPrefabs[Random.Range(0, ItemPrefabs.Count)];
        Instantiate(item, ItemSpawner.transform.position, Quaternion.identity);
    }
}
