using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour {

	public Image overlayImage;
	public Color fadeColor = new Color(0, 0, 0);
	public float fadeSpeed = 1.5f;
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
		overlayImage.color = Color.Lerp(overlayImage.color, fadeColor, fadeSpeed * Time.deltaTime);
	}

	public void FadeToClear()
	{
		//Lerp the color of the overlay image to clear, so that it is transparent.
		overlayImage.color = Color.Lerp(overlayImage.color, Color.clear, fadeSpeed * Time.deltaTime);

		if(overlayImage.color.a == 0)
		{
			isFading = false;
		}
	}

	public IEnumerator GoToScene(string sceneName)
	{
		FadeToColor();
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneName);
	}
}