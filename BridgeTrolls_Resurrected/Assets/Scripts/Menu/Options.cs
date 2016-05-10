using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {

	public Toggle fullScreenToggleUI;
	public Slider musicSliderUI;
	public Slider soundFxSliderUI;
	public Dropdown resolutionDropdownUI;
	public Dropdown qualityDropdownUI;

	AudioSource[] _audioSources;
	bool _fullScreen = true;
	bool _showTutorial = true;

	int _xSizeResolution = 1920;
	int _ySizeResolution = 1080;

	void Awake()
	{
		_audioSources = GameObject.Find("Audio Handler").GetComponents<AudioSource>();
		PlayerPrefs.SetString("ShowTutorial", "True");
		//PlayerPrefs.SetInt("QualitySetting", 5);
	}

	void FixedUpdate()
	{
		Screen.fullScreen = _fullScreen;
	}

	public void FullScreenToggle()
	{
		_fullScreen = !_fullScreen;
		print("Full Screen is " + Screen.fullScreen);
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
		_showTutorial = !_showTutorial;
		PlayerPrefs.SetString("ShowTutorial", _showTutorial.ToString());
	}

	public void SetResolution()
	{
		if(resolutionDropdownUI.value == 0)
			Screen.SetResolution(1920, 1080, _fullScreen);
		else if(resolutionDropdownUI.value == 1)
			Screen.SetResolution(1280, 720, _fullScreen);
		else if(resolutionDropdownUI.value == 2)
			Screen.SetResolution(1024, 576, _fullScreen);
	}

	public void SetQuality()
	{
		if(qualityDropdownUI.value == 0)
		{
			QualitySettings.SetQualityLevel(5);
			PlayerPrefs.SetInt("QualitySetting", 5);
		}
		else if(qualityDropdownUI.value == 1)
		{
			QualitySettings.SetQualityLevel(3);
			PlayerPrefs.SetInt("QualitySetting", 3);
		}
		else if(qualityDropdownUI.value == 2)
		{
			QualitySettings.SetQualityLevel(0);
			PlayerPrefs.SetInt("QualitySetting", 0);
		}
	}
}