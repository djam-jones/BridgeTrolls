using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public string sceneName;
	public float waitingSeconds;
	private float _timer;
	FadeScreen _fade;
	ScreenManager _screenManager;

	void Awake()
	{
		_fade = GameObject.Find("Fade Object").GetComponent<FadeScreen>();
		_screenManager = GameObject.Find("Fade Object").GetComponent<ScreenManager>();
		_timer = 0f;
	}

	void Update()
	{
		StartCoroutine( LoadLevel(sceneName) );
	}

	private IEnumerator LoadLevel(string scene)
	{
		_timer += Time.deltaTime;
		if(_timer > waitingSeconds)
		{
			_fade.FadeToColor();
			yield return new WaitForSeconds(waitingSeconds);
			StartCoroutine( _screenManager.LoadScene(scene) );
			yield break;
		}
	}

}