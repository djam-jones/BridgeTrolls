using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadyPlayers : MonoBehaviour {

	[SerializeField] public List<HubHandler> readyPlayers = new List<HubHandler>();
	[SerializeField] public List<HubHandler> playersInCharacterSelect = new List<HubHandler>();

	public GameObject startGameBanner;
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

	private bool startedGame = false;

	private GameModes GameModesInstance;
	public static ReadyPlayers Instance {get; private set;}

	void Awake()
	{
		CheckForInstance();
		GameModesInstance = GetComponent<GameModes>();
	}

	void Update()
	{
		CheckReadyPlayers();

		if(startedGame)
		{
			StartCoroutine( GameObject.Find("Main Camera").GetComponent<FadeScreen>().GoToScene("Main") );
		}
	}

	private void CheckReadyPlayers()
	{
		if(readyPlayers.Count >= 2 && playersInCharacterSelect.Count == 0)
		{
			//Display Start Banner.
			if(!startGameBanner.activeInHierarchy && startGameBanner != null)
			{
				startGameBanner.SetActive(true);
			}
			else{}
			//Set a Button to Start The Game.
			if(Input.GetButtonDown(START_GAME_BUTTON))
			{
				//Save the Game Mode.
				PlayerPrefs.SetInt("GameMode", GameModesInstance.gameModeIndex);

				//Save the game points.
				PlayerPrefs.SetInt("AmountOfGamePoints", GameModesInstance.amountOfGamePoints);

				//Save the Amount of Players playing.
				PlayerPrefs.SetInt("PlayerCount", readyPlayers.Count);
				startedGame = true;
			}
		}
		else
		{
			//Hide Start Banner
			if(startGameBanner.activeInHierarchy && startGameBanner != null)
			{
				startGameBanner.SetActive(false);
			}
			else{}
		}
	}

	private void CheckForInstance()
	{
		//Check if there are any conflicting instances
		if(Instance != null && Instance != this)
		{
			//If so Destroy those instances.
			Destroy(gameObject);
		}
		//Save Singleton instance.
		Instance = this;

//		DontDestroyOnLoad(gameObject);
	}
}