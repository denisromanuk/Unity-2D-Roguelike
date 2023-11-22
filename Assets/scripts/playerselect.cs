using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerselect : MonoBehaviour
{
    // Start is called before the first frame update

    public void Blue()
    {
        PlayerPrefs.SetString("player", "blue");
    }

    public void Green()
    {
        PlayerPrefs.SetString("player", "green");
    }

    public void Red()
    {
        PlayerPrefs.SetString("player", "red");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
