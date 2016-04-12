using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour {

	//If this is the Menu Scene
	//Save the value of the Music & SFX Slider/volume
	//Else if this is the Main Scene
	//'Load' the value of the Music & SFX Slider/volume

	AudioSource[] _audioSources;
	Scene _scene;

	void Awake()
	{
		_audioSources = GetComponents<AudioSource>();

		if(_scene.name == "MainMenu")
		{
			PlayerPrefs.SetFloat("MusicVolume", _audioSources[0].volume);
			PlayerPrefs.SetFloat("SFXVolume", _audioSources[1].volume);
		}
		else if(_scene.name == "Main")
		{
			PlayerPrefs.GetFloat("MusicVolume");
			PlayerPrefs.GetFloat("SFXVolume");
		}
	}

}