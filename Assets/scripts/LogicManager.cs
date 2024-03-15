using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    [Header("Characters:")]
    public GameObject bluePrefab;
    public GameObject greenPrefab;
    public GameObject redPrefab;

    [Header("Hearts:")]
    public GameObject[] hearts;
    public Sprite heart_full;
    public Sprite heart_half;
    public Sprite heart_empty;
    int fullHpCount;
    int halfHpCount;

    [Header("Stats:")]
    public TMP_Text _statsDMG;
    public TMP_Text _statsFIRERATE;
    public TMP_Text _statsSPEED;

    [Header("Item screen:")]
    public GameObject ItemInfo;
    public TMP_Text itemName;
    public TMP_Text itemDesc;


    [Header("UI:")]
    public GameObject DeathScreen;
    public GameObject VictoryScreen;
    public TMP_Text _stage;

    Player _player;
    GameObject p;
    AudioManager _audiomanager;
    

    void Awake() 
    {
        //play level music on awake:
        _audiomanager = FindAnyObjectByType<AudioManager>().GetComponent<AudioManager>();
        _audiomanager._musicSource.clip = _audiomanager.level;
        _audiomanager._musicSource.Play();

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

        //enable stats TMP:
        _statsDMG.GetComponent<TMP_Text>().enabled = true;
        _statsFIRERATE.GetComponent<TMP_Text>().enabled = true;
        _statsSPEED.GetComponent<TMP_Text>().enabled = true;

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
        _statsDMG.text = _player.dmg.ToString();
        _statsFIRERATE.text = _player.fireRate.ToString();
        _statsSPEED.text = _player.speed.ToString();

        _stage.text = $"Stage {SceneManager.GetActiveScene().buildIndex - 1}";


        if(_player.IsDestroyed())
        {
            DeathScreen.SetActive(true);
        }

        if(fullHpCount != _player.hp || halfHpCount != _player.hp){
            fullHpCount = (int)_player.hp / 2; // 5/2 = 2
            halfHpCount = (int)_player.hp % 2;// 5/2 = 1
            hpUI();
        }
    }

    void hpUI()
    {
        for (int i = 0; i < hearts.Length; i++){
            hearts[i].SetActive(false);
        }
        for (int i = 0; i < fullHpCount; i++)
        {
            hearts[i].SetActive(true);
            hearts[i].GetComponent<Image>().sprite = heart_full;
        }
        if(halfHpCount != 0){
            hearts[fullHpCount].SetActive(true);
            hearts[fullHpCount].GetComponent<Image>().sprite = heart_half;
        }
        if(_player.hp <= 0){
            hearts[0].SetActive(true);
            hearts[0].GetComponent<Image>().sprite = heart_empty;
        }
    }

    public void Item(string itemname){
        ItemInfo.SetActive(true);
        Invoke("ItemClose", 2f);
        Debug.Log(itemname);
        
        switch(itemname)
        {
            case "AnabolicSteroids(Clone)":
                itemName.text = "Anabolic Steroids";
                itemDesc.text = $"'100% natty' \n DMG + \n HP -";
                break;
            case "Beer(Clone)":
                itemName.text = "Beer";
                itemDesc.text = $"'Cheers' \n HP +";
                break;
            case "Crocs(Clone)":
                itemName.text = "Crocs";
                itemDesc.text = $"'They're in sport mode' \n SPEED +";
                break;
            case "EnergyDrink(Clone)":
                itemName.text = "Energy Drink";
                itemDesc.text = $"FIRERATE + \n SPEED +";
                break;
            case "Lighter(Clone)":
                itemName.text = "Lighter";
                itemDesc.text = $"DMG +";
                break;
            case "OldPaper(Clone)":
                itemName.text = "Old Paper";
                itemDesc.text = $"FIRERATE +";
                break;
        }
    }

    void ItemClose(){
        ItemInfo.SetActive(false);
    }

    public void Restart(){
        _audiomanager._musicSource.Stop();
        SceneManager.LoadScene(2);
    }

    public void ReturnToMenu(){
        Destroy(_audiomanager.gameObject);
        SceneManager.LoadScene(0);
    }

    public void Victory(){
        VictoryScreen.SetActive(true);
    }
}
