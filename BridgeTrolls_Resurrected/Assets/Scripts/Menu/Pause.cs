using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	//Start Button
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

	[SerializeField] private GameMagager manager;

	void Awake()
	{
		pauseMenu.SetActive(false);
		manager = GetComponent<GameMagager>();
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P) && pauseMenu.activeInHierarchy == false || Input.GetButtonDown(START_GAME_BUTTON) && pauseMenu.activeInHierarchy == false)
        {
			PauseGame();
        }
		else if (Input.GetKeyDown(KeyCode.P) && pauseMenu.activeInHierarchy == true || Input.GetButtonDown(START_GAME_BUTTON) && pauseMenu.activeInHierarchy == true)
        {
			ContinueGame();
        }
    }

	public void PauseGame()
	{
		pauseMenu.SetActive(true);
		
		//Disable Movement for every Player.
		for(int i = 0; i < manager.playerArray.Count; i++)
		{
			manager.playerArray[i].GetComponent<Movement>().enabled = false;
			manager.playerArray[i].GetComponent<AbilityHandler>().enabled = false;
		}
		
		GetComponent<DeathWall>().enabled = false;
	}

	public void ContinueGame()
	{
		pauseMenu.SetActive(false);

		//Enable Movement for every Player.
		for(int i = 0; i < manager.playerArray.Count; i++)
		{
			manager.playerArray[i].GetComponent<Movement>().enabled = true;
			manager.playerArray[i].GetComponent<AbilityHandler>().enabled = true;
		}

		GetComponent<DeathWall>().enabled = true;
	}
}