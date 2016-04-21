using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

    private AudioHandler _audioHandler;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioHandler = GameObject.Find("Audio Handler").GetComponent<AudioHandler>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void LetsPlayMySound(int audioFragment)
    {
        _audioSource.clip = _audioHandler.allSoundEffects[audioFragment];
        _audioSource.Play();
    }
}
