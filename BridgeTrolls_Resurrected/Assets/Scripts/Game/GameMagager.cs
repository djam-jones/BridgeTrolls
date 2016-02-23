using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMagager : MonoBehaviour {
	
    public GameMagager manager;
    [SerializeField]private int amountPlayers;
    public List<GameObject> playerArray = new List<GameObject>(); 
    [SerializeField]GameObject prefabPlayer;
    [SerializeField]GameObject prefabEnemy;

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
		GameObject enemyPlayer = playerArray[Random.Range(0, playerArray.Count)];
		enemyPlayer = Instantiate(prefabEnemy, new Vector2(0, 0), prefabEnemy.transform.rotation) as GameObject;
		playerArray.Add(enemyPlayer);
	}

	private void InstantiatePlayersAndAddToList()
	{
		GameObject playerprefab = Instantiate(prefabPlayer, new Vector2(-8.4f,Random.Range(4.5f,-3.5f)), prefabPlayer.transform.rotation) as GameObject;
		playerArray.Add(playerprefab);
	}

	public int GetPlayerId
	{
		get
		{
			int id = playerArray.Count;
			return id;
		}
		set{}
	}
}
