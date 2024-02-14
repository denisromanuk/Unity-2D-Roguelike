using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public GameObject bluePrefab;
    public GameObject greenPrefab;
    public GameObject redPrefab;
    Player _player;
    GameObject p;
    TMP_Text _stats;

    void Awake() 
    {
        switch(PlayerPrefs.GetInt("selectedPlayer"))
        {
            case 1:
                InstantiatePlayer(bluePrefab);
                break;
            case 2:
                InstantiatePlayer(greenPrefab);
                break;
            case 3:
                InstantiatePlayer(redPrefab);
                break;
        }

        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<TMP_Text>();
        _stats.GetComponent<TMP_Text>().enabled = true;
    }

    void InstantiatePlayer(GameObject playerprefab)
    {
        p = Instantiate(playerprefab, new Vector3(0, -2, 10), Quaternion.identity);
        _player = p.GetComponent<Player>();
    }

    void Update() 
    {
        //hp:  |  dmg:  |  speed:  |  fire rate:
        _stats.text = $"hp: {_player.hp} |  dmg: {_player.dmg} |  speed: {_player.speed} |  fire rate: {_player.fireRate}";
    }
}
