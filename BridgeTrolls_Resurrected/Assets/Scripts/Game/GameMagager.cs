using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMagager : MonoBehaviour {
	
    public GameMagager manager;

    [SerializeField]private int amountPlayers;
    public List<GameObject> playerArray = new List<GameObject>(); 
    [SerializeField]GameObject prefabPlayer;
    [SerializeField]GameObject prefabEnemy;

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
		for (int i = 0; i < amountPlayers; i++)
		{
			InstantiatePlayersAndAddToList();
		}
		SetEnemyPlayer();
	}

	private void SetEnemyPlayer()
	{
		GameObject enemyPlayer = playerArray[Random.Range(0, playerArray.Count)]; 	//Pick a random player from the Player List.
		enemyPlayer.GetComponent<PlayerRoles>().playerRoles = Roles.Hostile; 		//Set Role to Hostile and thus the Troll.

		enemyPlayer.name = "Troll";													//Set Name to Troll.
		enemyPlayer.tag = "Enemy";													//Set Tag to Enemy.

		enemyPlayer.transform.position = new Vector2(0, 0); 						//Set Position to zero.
		enemyPlayer.GetComponent<SpriteRenderer>().color = Color.red; 				//TODO: Change this to sprites eventually. But for now only change color.
	}

	private void InstantiatePlayersAndAddToList()
	{
		GameObject playerprefab = Instantiate(prefabPlayer, new Vector2(-8.4f, Random.Range(4.5f,-3.5f)), prefabPlayer.transform.rotation) as GameObject;
		playerArray.Add(playerprefab);
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