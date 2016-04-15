using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadyPlayers : MonoBehaviour {

	[SerializeField] public List<HubHandler> readyPlayers = new List<HubHandler>();
	[SerializeField] public List<HubHandler> playersInCharacterSelect = new List<HubHandler>();

	public GameObject startGameBanner;
	public GameObject transitionLockDoors;
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

	private bool startedGame = false;

	private FadeScreen _fadeScript;
	private ScreenManager _screenManager;
	private GameModes GameModesInstance;
	public static ReadyPlayers Instance {get; private set;}

	void Awake()
	{
		CheckForInstance();
		GameModesInstance = GetComponent<GameModes>();
		_fadeScript = GameObject.Find("Main Camera").GetComponent<FadeScreen>();
		_screenManager = GameObject.Find("Loading Screen Manager").GetComponent<ScreenManager>();
	}

	void Start()
	{
		transitionLockDoors.SetActive(false);
	}

	void Update()
	{
		CheckReadyPlayers();

		if(startedGame)
		{
			StartCoroutine( _screenManager.LoadScene("Main") );
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
				transitionLockDoors.SetActive(true);
				GameObject.Find("Audio Handler").GetComponents<AudioSource>()[0].Stop();

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