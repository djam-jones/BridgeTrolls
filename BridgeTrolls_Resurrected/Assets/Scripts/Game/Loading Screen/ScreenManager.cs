using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	[SerializeField] private string _scene = "";

	public IEnumerator LoadScene(string sceneName)
	{
		//Load the Loading Screen scene.
		SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);

		//An Asynchronous operation to load the appointed scene.
		AsyncOperation aSync = SceneManager.LoadSceneAsync(sceneName);

		//After this, unload the Loading Screen scene. But only if the AsyncOperation has finished its course.
		if(aSync.isDone)
			SceneManager.UnloadScene("LoadingScene");

		while(!aSync.isDone)
			yield return null;

		yield break;
	}
}