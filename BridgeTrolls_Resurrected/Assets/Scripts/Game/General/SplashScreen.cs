using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public string sceneName;
	public float waitingSeconds;
	private float _timer;
	private bool _doLoading = false;
	FadeScreen _fade;
	ScreenManager _screenManager;

	void Awake()
	{
		_fade = GameObject.Find("Loading Screen Manager").GetComponent<FadeScreen>();
		_screenManager = GameObject.Find("Loading Screen Manager").GetComponent<ScreenManager>();
		_timer = 0f;

		//Delete all Player Preferences at the start of the game if it is not in the Editor.
		if(!Application.isEditor)
			PlayerPrefs.DeleteAll();
	}

	void Update()
	{
		_timer += Time.deltaTime;

		if(!_doLoading)
			LoadLevel(sceneName);
	}

	private void LoadLevel(string scene)
	{
		if(_timer > waitingSeconds)
		{
			_fade.FadeToColor();
			Wait(waitingSeconds);
			StartCoroutine( _screenManager.LoadScene(scene) );
			_doLoading = true;
		}
	}

	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}

}