using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour {

	public Image overlayImage;
	public float fadeSpeed = 1.5f;
	public bool sceneStarting = true;

	public static FadeScreen Instance {get; private set;}

	void Awake()
	{
		CheckInstance();
		overlayImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
	}

	void Update()
	{
		if(sceneStarting)
			StartScene();
	}

	private void FadeToBlack()
	{
		//Lerp the color of the overlay image to black.
		overlayImage.color = Color.Lerp(overlayImage.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	private void FadeToClear()
	{
		//Lerp the color of the overlay image to clear, so that it is transparent.
		overlayImage.color = Color.Lerp(overlayImage.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void StartScene()
	{
		//Fade our image to Clear.
		FadeToClear();

		if(overlayImage.color.a <= 0.05f)
		{
			overlayImage.color = Color.clear;
			overlayImage.enabled = false;

			//The scene is not starting.
			sceneStarting = false;
		}
	}

	public void EndScene(int sceneNum)
	{
		//Enable our overlaying Image.
		overlayImage.enabled = true;

		//Fade it to black.
		FadeToBlack();

		if(overlayImage.color.a >= 0.95f)
		{
			overlayImage.color = Color.black;
			SceneManager.LoadScene(sceneNum);
		}
	}



	private void CheckInstance()
	{
		//Check if there are any conflicting instances
		if(Instance != null && Instance != this)
		{
			//If so Destroy those instances.
			Destroy(gameObject);
		}
		//Save Singleton instance.
		Instance = this;

		DontDestroyOnLoad(gameObject);
	}
}