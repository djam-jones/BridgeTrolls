using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {

	public void FullScreenToggle(Toggle isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
			
		if(!isFullscreen)
			Screen.SetResolution(1152, 648, isFullscreen);
		else if(isFullscreen)
			Screen.SetResolution(1920, 1080, isFullscreen);

		print("Fullscreen is " + Screen.fullScreen);
	}

	public void MusicVolume()
	{
		//Do Something Cool.
	}

	public void SoundVolume()
	{
		//Do Something Cool.
	}

	public void Controls()
	{
		//Do Something Cool.
	}

	public void ShowTutorialToggle()
	{
		//Do Something Cool.
	}
}