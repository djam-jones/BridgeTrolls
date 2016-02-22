using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMagager : MonoBehaviour {
    public GameMagager manager;
    [SerializeField]private int amountPlayers;
    public List<GameObject> playerArray = new List<GameObject>(); 
    [SerializeField]GameObject prefabPlayer;
    [SerializeField]GameObject prefabEnemy;
    void Start()
    {
        for (int i = 0; i < amountPlayers -1; i++)
        {
            GameObject playerprefab = Instantiate(prefabPlayer, new Vector2(-8.4f,Random.Range(4.5f,-3.5f)), prefabPlayer.transform.rotation) as GameObject;
            playerArray.Add(playerprefab);
        }

        Instantiate(prefabEnemy, new Vector2(0, 0), prefabEnemy.transform.rotation);
        
    }
	
	void Update ()
    {

	}
}
