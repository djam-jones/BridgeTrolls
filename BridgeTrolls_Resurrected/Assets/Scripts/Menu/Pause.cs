using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu;
	//Start Button
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

	void Awake()
	{
		pauseMenu.SetActive(false);
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P) && pauseMenu.activeInHierarchy == false || Input.GetButtonDown(START_GAME_BUTTON) && pauseMenu.activeInHierarchy == false)
        {
            Debug.Log("disable");
			pauseMenu.SetActive(true);

			//Disable Movement for every Player.
			for(int i = 0; i < GetComponent<GameMagager>().playerArray.Count; i++)
			{
				GetComponent<GameMagager>().playerArray[i].GetComponent<Movement>().enabled = false;
				GetComponent<GameMagager>().playerArray[i].GetComponent<AbilityHandler>().enabled = false;
			}
            
			GetComponent<DeathWall>().enabled = false;
        }
		else if (Input.GetKeyDown(KeyCode.P) && pauseMenu.activeInHierarchy == true || Input.GetButtonDown(START_GAME_BUTTON) && pauseMenu.activeInHierarchy == true)
        {
			ContinueGame();
        }
    }

	public void ContinueGame()
	{
		pauseMenu.SetActive(false);

		//Enable Movement for every Player.
		for(int i = 0; i < GetComponent<GameMagager>().playerArray.Count; i++)
		{
			GetComponent<GameMagager>().playerArray[i].GetComponent<Movement>().enabled = true;
			GetComponent<GameMagager>().playerArray[i].GetComponent<AbilityHandler>().enabled = true;
		}

		GetComponent<DeathWall>().enabled = true;
	}
}