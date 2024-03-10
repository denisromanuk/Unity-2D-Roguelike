using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    [Header("Characters:")]
    public GameObject bluePrefab;
    public GameObject greenPrefab;
    public GameObject redPrefab;

    [Header("UI:")]
    public GameObject DeathScreen;
    public GameObject VictoryScreen;
    public TMP_Text _stage;

    Player _player;
    GameObject p;
    TMP_Text _stats;

    void Awake() 
    {
        //create player only in 1st stage:
        if(SceneManager.GetActiveScene().buildIndex == 2)
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

        if(SceneManager.GetActiveScene().buildIndex > 2){
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

        _stage.text = $"Stage {SceneManager.GetActiveScene().buildIndex - 1}";
        

        if(_player.IsDestroyed())
        {
            DeathScreen.SetActive(true);
            //Time.timeScale = 0f; //freeze time
        }
    }

    public void Restart(){
        SceneManager.LoadScene(2);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }

    public void Victory(){
        VictoryScreen.SetActive(true);
    }
}
