using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GameMagager : MonoBehaviour {
	
    public GameMagager manager;
	[HideInInspector] public bool isMainScene;
	private bool _gamePaused;
	private bool _gameStarted = false;
	[HideInInspector]
	public bool gameOver = false;
	[HideInInspector]
	public bool trollWins;
//	[HideInInspector]
	public int currentRound = 0;

	//Start Button
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

    [SerializeField]private int amountPlayers;
    public List<GameObject> playerArray = new List<GameObject>(); 

	public List<GameObject> allGoblins = new List<GameObject>();
	public List<GameObject> allMinions = new List<GameObject>();

	public List<GameObject> rightSidedPlayers = new List<GameObject>();

	public Sprite trollSprite;

	//Win Screen
	public GameObject winScreen;
	public Text winScreenText;
	public GameObject trollWinScreen;
	public Text trollWinScreenText;

	public GameObject transitionLock;
	public GameObject eventSystem;

	[SerializeField] public Text trollIndicationText;

	[SerializeField] public Image directionIndicator;
	[SerializeField] public Text directionText;
	[SerializeField] public Text roundIndicator;

	private RectTransform _rectTransform;
	private Vector3 _rectTransformScale;

	public FadeScreen fadeScript;
	public AudioHandler audioHandlerScript;
	public ScreenManager _screenManager;

	private int id = 0;

	public static GameMagager Instance {get; private set;}

	void Awake()
	{
		CheckForInstance();

		amountPlayers = PlayerPrefs.GetInt("PlayerCount");

		Scene _scene = SceneManager.GetActiveScene();
		if(_scene.name == "Main")
		{
			CheckAmountOfPlayers();
			isMainScene = true;
		}
		else if(_scene.name == "MainMenu")
		{
			isMainScene = false;
		}

		_screenManager = GameObject.Find("Loading Screen Manager").GetComponent<ScreenManager>();

		//Disable all Player Movement at the Awake.
		for(int i = 0; i < playerArray.Count; i++)
		{
			playerArray[i].GetComponent<Movement>().enabled = false;
			playerArray[i].GetComponent<AbilityHandler>().enabled = false;
		}

		if(isMainScene) 
		{
			//Disable all Gameplay Scripts at the Awake.
			GetComponent<DeathWall>().enabled = false;
			GetComponent<Pause>().enabled = false;

			winScreen.SetActive(false);
			trollWinScreen.SetActive(false);

			transitionLock.SetActive(true);
			audioHandlerScript.GetComponents<AudioSource>()[1].clip = audioHandlerScript.allSoundEffects[0];
			audioHandlerScript.GetComponents<AudioSource>()[1].Play();
			transitionLock.GetComponent<Animator>().Play("Transition_Open");

			_rectTransform = directionIndicator.GetComponent<RectTransform>();
			_rectTransformScale = _rectTransform.localScale;
			_rectTransform.localScale = _rectTransformScale;

			//QualitySettings.SetQualityLevel( PlayerPrefs.GetInt("QualitySetting") );
		}
	}

	void Update()
	{
		if(isMainScene)
		{
			GetAllPlayerRoles();
			GameOver();

			roundIndicator.text = currentRound.ToString() + " / " + PlayerPrefs.GetInt("AmountOfGamePoints").ToString();

			if(rightSidedPlayers.Count != allGoblins.Count)
			{
				_rectTransformScale.x = -1;
				directionText.text = "Move Right!";
			}
			else if(rightSidedPlayers.Count == allGoblins.Count)
			{
				_rectTransformScale.x = 1;
				directionText.text = "Move Left!";
			}
		}
		ForceQuitGame();
	}
	
	public void CheckAmountOfPlayers ()
    {
		InstantiatePlayersAndAddToList();
		SetEnemyPlayer();
	}

    private void SetEnemyPlayer()
    {
        GameObject enemyPlayer = playerArray[Random.Range(0, playerArray.Count)];   //Pick a random player from the Player List.
        enemyPlayer.GetComponent<PlayerRoles>().playerRoles = Roles.Hostile;        //Set Role to Hostile and thus the Troll.
        enemyPlayer.GetComponent<Movement>().speed = (enemyPlayer.GetComponent<Movement>().speed - 1f);

        enemyPlayer.name = enemyPlayer.name + " Troll";                             //Set Name to Troll.
        enemyPlayer.tag = Tags.TROLL_TAG;                                           //Set Tag to Enemy.

        enemyPlayer.transform.position = new Vector2(0, 0);                         //Set Position to zero.
        enemyPlayer.GetComponent<SpriteRenderer>().sprite = trollSprite;            //Change sprite to the Troll sprite.

        enemyPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/Characters/Troll_Animator") as RuntimeAnimatorController;
        enemyPlayer.transform.GetChild(2).transform.position = new Vector2(0, 1.75f);

		//Set the Box Collider needs to those of the Troll.
		BoxCollider2D _boxCollider = enemyPlayer.GetComponents<BoxCollider2D>()[1];
		Vector2 _boxColliderOffset;
		Vector2 _boxColliderSize;

		_boxColliderOffset.x = -0.231294f;
		_boxColliderOffset.y = -0.769804f;
		_boxColliderSize.x = 1f;
		_boxColliderSize.y = 1f;

		_boxCollider.offset = _boxColliderOffset;
		_boxCollider.size = _boxColliderSize;

		//Set the Box Trigger needs to those of the Troll.
		BoxCollider2D _boxTrigger = enemyPlayer.GetComponents<BoxCollider2D>()[0];
		Vector2 _boxTriggerOffset;
		Vector2 _boxTriggerSize;
        
		_boxTriggerOffset.x = -0.1739898f;
		_boxTriggerOffset.y = -0.8419237f;
		_boxTriggerSize.x = 2.636648f;
		_boxTriggerSize.y = 0.75f;

		_boxTrigger.offset = _boxTriggerOffset;
		_boxTrigger.size = _boxTriggerSize;
	}

	private void InstantiatePlayersAndAddToList()
	{
		string playerCharacter;

		for(int i = 0; i < amountPlayers; i++)
		{
			playerCharacter = PlayerPrefs.GetString("CharacterName" + i);

			GameObject playerPrefab = PlayerFactory.CreatePlayer(playerCharacter, i);
			Player playerScript = playerPrefab.GetComponent<Player>();

			playerPrefab.transform.position = new Vector2(-8.4f, Random.Range(5f, -5f));

			playerPrefab.name = "Player " + (i + 1); //Set the Player Name to PLAYER_NUM
			playerPrefab.tag = Tags.PLAYER_TAG;
			playerPrefab.GetComponent<PlayerRoles>().playerRoles = Roles.Neutral;
			playerScript.SetCharacter(playerCharacter);

			//Add the Player Prefab to the Player Array.
			playerArray.Add(playerPrefab);
		}
	}

	private void GetAllPlayerRoles()
	{
		foreach(GameObject player in playerArray)
		{
			if(player.GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && !allGoblins.Contains(player))
			{
				allGoblins.Add(player); //Adds the player gameObject to the AllGoblins List.
			}
			else if(player.GetComponent<PlayerRoles>().playerRoles == Roles.Minion && allGoblins.Contains(player) && !allMinions.Contains(player))
			{
				allGoblins.Remove(player);  //Removes the player gameObject from the AllGoblins List.
				allMinions.Add(player);		//And adds them to the AllMinions List.
			}
		}
	}

	public void ReturnToMainMenu()
	{
		audioHandlerScript.GetComponents<AudioSource>()[0].Stop();
		transitionLock.GetComponent<Animator>().Play("Transition_Close");
		StartCoroutine( _screenManager.LoadScene("MainMenu") );
	}

	public void Retry()
	{
		audioHandlerScript.GetComponents<AudioSource>()[0].Stop();
		transitionLock.GetComponent<Animator>().Play("Transition_Close");
		StartCoroutine( Wait(2) );
		StartCoroutine(_screenManager.LoadScene("Main"));
	}

	public void StartGame(bool gameStarted)
	{
		_gameStarted = gameStarted;

		//Disable all Player Movement.
		for(int i = 0; i < playerArray.Count; i++)
		{
			if(_gameStarted)
				playerArray[i].GetComponent<Movement>().enabled = true;
				playerArray[i].GetComponent<AbilityHandler>().enabled = true;
		}

		//Enable all GamePlay Scripts.
		GetComponent<DeathWall>().enabled = true;
		GetComponent<Pause>().enabled = true;
	}

	private void GameOver()
	{
		if(gameOver)
		{
			//Win Screen Pop-Up.
			if(!trollWins)
			{
				winScreen.SetActive(true);
				winScreenText.text = PlayerPrefs.GetString("PlayerThatWon") + " Wins!";

//				eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = winScreen.transform.GetChild(1).gameObject;
//				eventSystem.GetComponent<EventSystem>().SetSelectedGameObject( winScreen.transform.GetChild(1).gameObject );
			}
			else if(trollWins)
			{
				trollWinScreen.SetActive(true);
				trollWinScreenText.text = "Troll Wins!";

//				eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = trollWinScreen.transform.GetChild(1).gameObject;
//				eventSystem.GetComponent<EventSystem>().SetSelectedGameObject( trollWinScreen.transform.GetChild(1).gameObject );
			}

			//Disable all Player Movement.
			for(int i = 0; i < playerArray.Count; i++)
			{
				playerArray[i].GetComponent<Movement>().enabled = false;
				playerArray[i].GetComponent<AbilityHandler>().enabled = false;
			}

			//Disable all GamePlay Scripts.
			GetComponent<DeathWall>().enabled = false;
			GetComponent<Pause>().enabled = false;
		}
		_gameStarted = false;
	}

	private IEnumerator TrollIndicationTextFade(GameObject troll)
	{
		trollIndicationText.text = troll.name + " is the Troll.";
		yield return new WaitForSeconds(1.5f);
		trollIndicationText.color = Color.Lerp(trollIndicationText.color, Color.clear, Mathf.PingPong(Time.time, 1));
	}

	private void ForceQuitGame()
	{
		float timer = 0;

		if(Input.GetKey(KeyCode.Space))
		{
			timer += Time.deltaTime;
			if(timer >= 6)
			{
				print("Application Quitting...");
				Application.Quit();
			}
		}
	}

	private IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
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

//	public int GetPlayerId
//	{
//		get
//		{
//			for(int i = 0; i < playerArray.Count; i++)
//			{
//				foreach(GameObject player in playerArray)
//				{
//					id = playerArray.IndexOf(player);
//					id = (id + 1);
//				}
//			}
//			return id;
//		}
//		set
//		{
//			id = value;
//		}
//	}
}