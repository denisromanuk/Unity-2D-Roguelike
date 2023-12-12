using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    /*
    public Player P;

    // Start is called before the first frame update
    void Start()
    {
        P = GetComponent<Player>();
        P = new Player();

        Debug.Log(PlayerPrefs.GetString("player"));
        CreatePlayer();
        Debug.Log("LM: " + P.Speed.ToString());
    }

    // Update is called once per frame
    public void CreatePlayer()
    {
        switch(PlayerPrefs.GetString("player"))
        {
            case "blue":
                P.Stats(6,12,4,30);
                break;
            case "green":
                P.Stats(4,8,6,22);
                break;
            case "red":
                P.Stats(2,19,4,30);
                break;
        }
    }*/

    Player _player;
    TMP_Text _stats;

    private void Awake() {
        //hp:  |  dmg:  |  speed:  |  fire rate:

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<TMP_Text>();
    }

    private void Update() {
        _stats.text = $"hp: {_player.hp} |  dmg: {_player.dmg} |  speed: {_player.speed} |  fire rate: {_player.fireRate}";
    }
}
