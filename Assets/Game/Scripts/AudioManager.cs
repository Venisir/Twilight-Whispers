using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private bool _enabled;

    [SerializeField]
    private AudioClip _menu;

    [SerializeField]
    private AudioClip _game;

    [SerializeField]
    private List<AudioClip> _levelSfxs;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioSource _sfxSource;
    
    void Start()
    {
        if(_enabled && !_audioSource.isPlaying)
            EnableGameMusic();
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

    public void PlaySFX(string name)
    {
        if (_sfxSource.isPlaying)
            return;

        AudioClip file = _levelSfxs.Where(x => x.name == name).SingleOrDefault();

        if (file != null)
        {
            _sfxSource.clip = file;
            _sfxSource.Play();
        }
    }
}
