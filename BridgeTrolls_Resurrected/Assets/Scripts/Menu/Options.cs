using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {

	public Toggle fullScreenToggleUI;
	public Slider musicSliderUI;
	public Slider soundFxSliderUI;

	AudioSource[] _audioSources;
	bool _fullScreen = true;

	int _xSizeResolution = 1920;
	int _ySizeResolution = 1080;

	void Awake()
	{
		_audioSources = GameObject.Find("Audio Handler").GetComponents<AudioSource>();

//		fullScreenToggleUI.onValueChanged.AddListener(FullScreenToggle);
	}

	void Update()
	{
		Screen.fullScreen = _fullScreen;
	}

	public void FullScreenToggle()
	{
		_fullScreen = !_fullScreen;
	}

	public void MusicVolume()
	{
		_audioSources[0].volume = musicSliderUI.value;
	}

	public void SoundVolume()
	{
		_audioSources[1].volume = soundFxSliderUI.value;
	}

	public void Controls()
	{
		//Do Something Cool.
	}

	public void ShowTutorialToggle()
	{
		//Do Something Cool.
	}

	public void SetResolution()
	{
		//Do Something Cool.
	}

	public void SetQuality()
	{
		
	}
}