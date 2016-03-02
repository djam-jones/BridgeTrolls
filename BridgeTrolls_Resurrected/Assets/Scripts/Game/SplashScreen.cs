using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public string sceneName;
	public float waitingSeconds;

	void Update()
	{
		StartCoroutine( LoadLevel(sceneName) );
	}

	IEnumerator LoadLevel(string scene)
	{
		GameObject.Find("Main Camera").GetComponent<FadeScreen>().FadeToColor();
		yield return new WaitForSeconds(waitingSeconds);
		SceneManager.LoadScene(scene);
		yield break;
	}

}