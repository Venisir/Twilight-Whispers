using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioClip _menu;

    [SerializeField]
    private AudioClip _game;
    
    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        EnableGameMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableGameMusic()
    {
        _audioSource.Stop();
        _audioSource.clip = _game;
        _audioSource.Play();
    }

    public void EnableMenuMusic()
    {
        _audioSource.Stop();
        _audioSource.clip = _menu;
        _audioSource.Play();
    }
}
