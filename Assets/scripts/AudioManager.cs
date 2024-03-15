using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _musicSource;
    public AudioSource _sfxSource;

    [Header("music:")]
    public AudioClip menu;
    public AudioClip level;
    public AudioClip boss;
    public AudioClip bossEnd;

    [Header("sfx:")]
    public AudioClip fire; //default volume: 0.4f
    public AudioClip hit;
    public AudioClip item;

    private void Start() {
        //Menu music:
        _musicSource.clip = menu;
        _musicSource.Play();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip sfx)
    {
        _sfxSource.volume = 1f;
        if(sfx == fire){
            _sfxSource.volume = 0.4f;
        }
        _sfxSource.clip = sfx;
        _sfxSource.Play();
    }
}
