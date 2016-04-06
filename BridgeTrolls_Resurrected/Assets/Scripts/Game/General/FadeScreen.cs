using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour {

	public Image overlayImage;
	public Color fadeColor = new Color(0, 0, 0, 1f);
	public float fadeSpeed;
	public float waitSeconds;
	public bool isFading = true;

	void Update()
	{
		if(isFading)
		{
			FadeToClear();
		}
	}

	public void FadeToColor()
	{
		//Lerp the color of the overlay image to black.
		overlayImage.color = Color.Lerp(overlayImage.color, fadeColor, Mathf.PingPong(Time.time, fadeSpeed));
	}

	public void FadeToClear()
	{
		//Lerp the color of the overlay image to clear, so that it is transparent.
		overlayImage.color = Color.Lerp(overlayImage.color, Color.clear, Mathf.PingPong(Time.time, fadeSpeed));

		if(overlayImage.color.a == 0f)
		{
			isFading = false;
		}
	}

	public IEnumerator GoToScene(string sceneName)
	{
		FadeToColor();
		yield return new WaitForSeconds(waitSeconds);
		SceneManager.LoadScene(sceneName);
	}
}