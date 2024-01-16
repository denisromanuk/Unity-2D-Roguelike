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
    TMP_Text _stats;

    private void Awake() {

        switch(PlayerPrefs.GetInt("selectedPlayer"))
        {
            case 1:
                Instantiate(bluePrefab, new Vector3(0, -2, 10), Quaternion.identity);
                break;
            case 2:
                Instantiate(greenPrefab, new Vector3(0, -2, 10), Quaternion.identity);
                break;
            case 3:
                Instantiate(redPrefab, new Vector3(0, -2, 10), Quaternion.identity);
                break;
        }


        //hp:  |  dmg:  |  speed:  |  fire rate:

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<TMP_Text>();
    }

    private void Update() {
        _stats.text = $"hp: {_player.hp} |  dmg: {_player.dmg} |  speed: {_player.speed} |  fire rate: {_player.fireRate}";
    }
}
