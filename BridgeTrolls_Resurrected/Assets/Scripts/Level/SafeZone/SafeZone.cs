using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SafeZone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players;
	public GameMagager gameManager;
	public DONOTUSEDeathWall deathWallScript;

	void Update()
	{
		CheckTriggerContent();
	}

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
		
	/// <summary>
	/// Checks if every non-troll/minion player is in this trigger.
	/// then enable all the goblin players' movement.
	/// </summary>
	private void CheckTriggerContent()
	{
		if(players.Count == gameManager.allGoblins.Count)
		{
			enablePlayerMovement();
			deathWallScript.Reset();
		}
	}
}