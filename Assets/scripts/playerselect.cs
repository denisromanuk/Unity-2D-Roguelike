using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerselect : MonoBehaviour
{
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
        SceneManager.LoadScene(1);
    }
}
