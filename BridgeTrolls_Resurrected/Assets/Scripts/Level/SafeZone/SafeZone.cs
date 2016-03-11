using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SafeZone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            enablePlayerMovement();
        }
    }

	private void CheckTriggerContent()
	{
		//Pseudo Code
		//if every non-troll player is in this
		//gameobject's trigger, 
		//then Enable the Player's Movement.
	}
}