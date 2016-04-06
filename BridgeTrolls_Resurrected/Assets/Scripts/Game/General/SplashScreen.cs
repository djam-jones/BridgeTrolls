using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public string sceneName;
	public float waitingSeconds;
	private float _timer;
	FadeScreen _fade;

	void Awake()
	{
		_fade = GetComponent<FadeScreen>();
		_timer = 0f;
	}

	void Update()
	{
		LoadLevel(sceneName);
	}

	private void LoadLevel(string scene)
	{
		_timer += Time.deltaTime;
		if(_timer > waitingSeconds)
		{
			StartCoroutine(_fade.GoToScene(scene));
		}
	}

}