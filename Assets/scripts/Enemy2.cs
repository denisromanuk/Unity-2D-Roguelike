using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public TextMeshPro countdownText;
    public GameObject splashRadius;
    public float countdown = 0f;
    private int time = 3;

    public bool startCountdown;

    void Awake() 
    {
        startCountdown = false;
    }

    private void FixedUpdate() 
    {
        if(startCountdown)
        {
            countdownText.text = time.ToString();
            countdown += 1f * Time.deltaTime;
            if(countdown > 0.5f)
            {
                countdown = 0f;
                time--;
            }

            if(time < 1){
                Explode();
            }
        }
    }

    void Explode()
    {
        Destroy(gameObject);
        splashRadius.SetActive(true);
    }
}
