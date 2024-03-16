using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2roomaudio : MonoBehaviour
{
    public int SplitsCount;
    private AudioManager _audiomanager;

    void Start()
    {
        _audiomanager = FindAnyObjectByType<AudioManager>().GetComponent<AudioManager>();
    }

    
    void Update()
    {
        //Debug.Log(SplitsCount);
        if(SplitsCount == 0){
            PlayDeathSfx();
            SplitsCount--; //-1 so it doesn't loop
        }
    }

    void PlayDeathSfx()
    {
        _audiomanager._musicSource.time = 140;
        _audiomanager._musicSource.volume = 0.4f;
        _audiomanager._musicSource.loop = false;

        _audiomanager.PlaySFX(_audiomanager.bossEnd);
    }
}
