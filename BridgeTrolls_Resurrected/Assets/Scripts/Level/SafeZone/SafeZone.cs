using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SafeZone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players;
	public GameMagager gameManager;

    public void enablePlayerMovement()
    {
        if(players.Count != 0)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<Movement>().enabled = true;
            }
            players.Clear();
        }
    }

    public void addDisabledPlayer(GameObject player)
    {
        players.Add(player);
        player.GetComponent<Movement>().enabled = false;
    }

    void Update()
    {
		CheckTriggerContent();
    }

	private void CheckTriggerContent()
	{
		//Pseudo Code
		//if every non-troll player is in this
		//gameobject's trigger, 
		//then Enable the Player's Movement.
		//Vergelijk De trigger content met de AllGoblins Lijst 
		//Van de Game Handler.

		if(players.Count == gameManager.allGoblins.Count)
		{
			enablePlayerMovement();
			DeathWall.Instance.Reset();
		}
	}
}