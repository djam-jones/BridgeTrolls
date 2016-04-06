using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameMagager : MonoBehaviour {
	
    public GameMagager manager;
	private bool _isMainScene = false;
	private bool _gamePaused;
	private bool _gameStarted = false;
	[HideInInspector]
	public bool gameOver = false;
	[HideInInspector]
	public bool trollWins;

	//Start Button
	[SerializeField] private const string START_GAME_BUTTON = "StartGameButton";

    [SerializeField]private int amountPlayers;
    public List<GameObject> playerArray = new List<GameObject>(); 

	public List<GameObject> allGoblins = new List<GameObject>();
	public List<GameObject> allMinions = new List<GameObject>();

	[HideInInspector]
	public List<GameObject> rightSidedPlayers = new List<GameObject>();

	public Sprite trollSprite;

	//Win Screen
	public GameObject winScreen;
	public Text winScreenText;
	public GameObject trollWinScreen;
	public Text trollWinScreenText;

	[SerializeField] public Text trollIndicationText;
	[SerializeField] public Image directionIndicator;
	private RectTransform _rectTransform;
	private float _rectTransformXScale;

	public FadeScreen _fadeScript;

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
			_isMainScene = true;
		}
		else if(_scene.name == "MainMenu")
		{
			_isMainScene = false;
		}

		//Disable all Player Movement at the Awake.
		for(int i = 0; i < playerArray.Count; i++)
		{
			playerArray[i].GetComponent<Movement>().enabled = false;
			playerArray[i].GetComponent<AbilityHandler>().enabled = false;
		}

		_rectTransform = directionIndicator.GetComponent<RectTransform>();
		_rectTransformXScale = _rectTransform.localScale.x;

		//Disable all Gameplay Scripts at the Awake.
		GetComponent<DeathWall>().enabled = false;
		GetComponent<Pause>().enabled = false;

		winScreen.SetActive(false);
		trollWinScreen.SetActive(false);
	}

	void Update()
	{
		if(_isMainScene)
		{
			GetAllPlayerRoles();
			GameOver();

			if(rightSidedPlayers.Count == allGoblins.Count)
			{
				_rectTransformXScale = -1;
			}
			else if(rightSidedPlayers.Count != allGoblins.Count)
			{
				_rectTransformXScale = 1;
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
		GameObject enemyPlayer = playerArray[Random.Range(0, playerArray.Count)]; 	//Pick a random player from the Player List.
		enemyPlayer.GetComponent<PlayerRoles>().playerRoles = Roles.Hostile; 		//Set Role to Hostile and thus the Troll.
		enemyPlayer.GetComponent<PlayerIndicator>().yValue = 1.5f;
		enemyPlayer.GetComponent<Movement>().speed = (enemyPlayer.GetComponent<Movement>().speed - 0.5f);

//		StartCoroutine(TrollIndicationTextFade(enemyPlayer));

		enemyPlayer.name = enemyPlayer.name + " Troll";								//Set Name to Troll.
		enemyPlayer.tag = Tags.TROLL_TAG;											//Set Tag to Enemy.

		enemyPlayer.transform.position = new Vector2(0, 0); 						//Set Position to zero.
		enemyPlayer.GetComponent<SpriteRenderer>().sprite = trollSprite; 			//Change sprite to the Troll sprite.

		enemyPlayer.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/Characters/Troll_Animator") as RuntimeAnimatorController;

		//Set the Box Collider needs to those of the Troll.
		BoxCollider2D _boxCollider = enemyPlayer.GetComponents<BoxCollider2D>()[0];
		Vector2 _boxColliderOffset;
		Vector2 _boxColliderSize;

		_boxColliderOffset.x = -0.1739898f;
		_boxColliderOffset.y = -0.8419237f;
		_boxColliderSize.x = 2.636648f;
		_boxColliderSize.y = 0.75f;

		_boxCollider.offset = _boxColliderOffset;
		_boxCollider.size = _boxColliderSize;

		//Set the Box Trigger needs to those of the Troll.
		BoxCollider2D _boxTrigger = enemyPlayer.GetComponents<BoxCollider2D>()[1];
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
		StartCoroutine( _fadeScript.GoToScene("MainMenu") );
	}

	public void Retry()
	{
		StartCoroutine( _fadeScript.GoToScene("Main") );
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
			}
			else if(trollWins)
			{
				trollWinScreen.SetActive(true);
				trollWinScreenText.text = PlayerPrefs.GetString("PlayerThatWon") + " Wins!";
			}
			_gameStarted = false;

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