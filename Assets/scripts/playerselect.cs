using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerselect : MonoBehaviour
{
    // Start is called before the first frame update

    public void Blue()
    {
        PlayerPrefs.SetInt("selectedPlayer", 1);
    }

    public void Green()
    {
        PlayerPrefs.SetInt("selectedPlayer", 2);
    }

    public void Red()
    {
        PlayerPrefs.SetInt("selectedPlayer", 3);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
