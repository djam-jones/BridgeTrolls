using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMagager : MonoBehaviour {
	
    public GameMagager manager;

    [SerializeField]private int amountPlayers;
    public List<GameObject> playerArray = new List<GameObject>(); 
    [SerializeField]GameObject prefabPlayer;
    [SerializeField]GameObject prefabEnemy;

	public Sprite trollSprite;

	private int id = 0;

	public static GameMagager Instance {get; private set;}

	void Awake()
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

    void Start()
    {
		CheckAmountOfPlayers();
    }
	
	public void CheckAmountOfPlayers ()
    {
//		for (int i = 0; i < amountPlayers; i++)
//		{
			InstantiatePlayersAndAddToList();
//		}
		SetEnemyPlayer();
	}

	private void SetEnemyPlayer()
	{
		GameObject enemyPlayer = playerArray[Random.Range(0, playerArray.Count)]; 	//Pick a random player from the Player List.
		enemyPlayer.GetComponent<PlayerRoles>().playerRoles = Roles.Hostile; 		//Set Role to Hostile and thus the Troll.

		enemyPlayer.name = enemyPlayer.name + " Troll";								//Set Name to Troll.
		enemyPlayer.tag = "Enemy";													//Set Tag to Enemy.

		enemyPlayer.transform.position = new Vector2(0, 0); 						//Set Position to zero.
		enemyPlayer.GetComponent<SpriteRenderer>().sprite = trollSprite; 			//TODO: Change this to sprites eventually. But for now only change color.
	}

	private void InstantiatePlayersAndAddToList()
	{
		string playerCharacter;

		for(int i = 0; i < amountPlayers; i++)
		{
			playerCharacter = PlayerPrefs.GetString("CharacterName");

			GameObject playerPrefab = PlayerFactory.CreatePlayer(playerCharacter, i);
			playerPrefab.transform.position = new Vector2(-8.4f, Random.Range(4.5f, -4.5f));
			Player playerScript = playerPrefab.GetComponent<Player>();

			playerPrefab.name = "Player" + (i + 1); //Set the Player Name to PLAYER_NUM
			playerPrefab.GetComponent<PlayerRoles>().playerRoles = Roles.Neutral;
			playerScript.SetCharacter(playerCharacter);

			//Add the Player Prefab to the Player Array.
			playerArray.Add(playerPrefab);
		}
	}

	public int GetPlayerId
	{
		get
		{
			for(int i = 0; i < playerArray.Count; i++)
			{
				foreach(GameObject player in playerArray)
				{
					id = playerArray.IndexOf(player);
					id = (id + 1);
				}
			}
			return id;
		}
		set
		{
			id = value;
		}
	}
}