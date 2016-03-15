using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    [SerializeField]private Canvas pauseMenu;
	//Start Button
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

	void Awake()
	{
		pauseMenu.enabled = false;
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P) && pauseMenu.enabled == false || Input.GetButtonDown(START_GAME_BUTTON) && pauseMenu.enabled == false)
        {
            Debug.Log("disable");
            pauseMenu.enabled = true;

			//Disable Movement for every Player.
			for(int i = 0; i < GetComponent<GameMagager>().playerArray.Count; i++)
			{
				GetComponent<GameMagager>().playerArray[i].GetComponent<Movement>().enabled = false;
			}
            
			GetComponent<DeathWall>().enabled = false;
        }
		else if (Input.GetKeyDown(KeyCode.P) && pauseMenu.enabled == true || Input.GetButtonDown(START_GAME_BUTTON) && pauseMenu.enabled == true)
        {
			ContinueGame();
        }
    }

	public void ContinueGame()
	{
		pauseMenu.enabled = false;

		//Enable Movement for every Player.
		for(int i = 0; i < GetComponent<GameMagager>().playerArray.Count; i++)
		{
			GetComponent<GameMagager>().playerArray[i].GetComponent<Movement>().enabled = true;
		}
	}
}