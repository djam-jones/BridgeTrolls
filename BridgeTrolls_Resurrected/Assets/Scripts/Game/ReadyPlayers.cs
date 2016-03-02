using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadyPlayers : MonoBehaviour {

	[SerializeField] public List<HubHandler> readyPlayers = new List<HubHandler>();
	public GameObject startGameBanner;
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

	private bool startedGame = false;

	public static ReadyPlayers Instance {get; private set;}

	void Awake()
	{
		CheckForInstance();
	}

	void Update()
	{
		CheckReadyPlayers();
	}

	private void CheckReadyPlayers()
	{
		if(readyPlayers.Count >= 2)
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
				//Load Level Scene
				print("Starting Game!");
				PlayerPrefs.SetInt("PlayerCount", readyPlayers.Count);
				startedGame = true;
			}
			if(startedGame)
			{
				StartCoroutine( GameObject.Find("Main Camera").GetComponent<FadeScreen>().GoToScene("Main") );
				startedGame = false;
			}
		}
		else
		{
			//Hide Start Banner
			if(startGameBanner.activeInHierarchy && startGameBanner != null)
			{
				startGameBanner.SetActive(false);
			}
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

		DontDestroyOnLoad(gameObject);
	}
}