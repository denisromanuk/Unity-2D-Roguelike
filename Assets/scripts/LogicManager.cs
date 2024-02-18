using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //create player only in 1st stage:
        if(SceneManager.GetActiveScene().buildIndex == 1)
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

            
        }

        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<TMP_Text>();
        _stats.GetComponent<TMP_Text>().enabled = true;

        if(SceneManager.GetActiveScene().buildIndex > 1){
            _player = FindAnyObjectByType<Player>().GetComponent<Player>();
        }
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
