using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	//Array for puns.
	public string[] trollPuns = new string[5];

	//Text for pun usage.
	public Text punText;

	//Image for Loading Bar
	public Image loadingBar;

	void Awake()
	{
		RandomQuote();
		StartCoroutine(LoadingLevel());
	}

	private void RandomQuote()
	{
		punText.text = trollPuns[ Random.Range(0, trollPuns.Length - 1) ];
	}

	IEnumerator LoadingLevel()
	{
		yield return new WaitForSeconds(4);
		AsyncOperation aSync = SceneManager.LoadSceneAsync(1);

		while(!aSync.isDone)
		{
			loadingBar.fillAmount = aSync.progress / 0.9f;
			yield return null;
		}
	}
}
