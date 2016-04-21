using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SafeZone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players;
	public GameMagager gameManager;
	public DeathWall deathWallScript;

	void Update()
	{
		CheckTriggerContent();
	}

    public void EnablePlayerMovement()
    {
        if(players.Count != 0 && deathWallScript.playtimer == true)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<Movement>().enabled = true;
				players[i].GetComponent<AbilityHandler>().enabled = true;
            }
			gameManager.currentRound += 1;
            players.Clear();
        }
    }

    public void AddDisabledPlayer(GameObject player)
    {
		DisablePlayer(player);
        players.Add(player);
    }

	public void DisablePlayer(GameObject player)
	{
		player.GetComponent<Movement>().enabled = false;
		player.GetComponent<AbilityHandler>().enabled = false;
	}
		
	/// <summary>
	/// Checks if every non-troll/minion player is in this trigger.
	/// then enable all the goblin players' movement.
	/// </summary>
	private void CheckTriggerContent()
	{
		if(players.Count == gameManager.allGoblins.Count)
		{
			EnablePlayerMovement();
			deathWallScript.Reset();
		}
	}
}