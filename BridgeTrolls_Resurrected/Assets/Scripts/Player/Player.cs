using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int playerNum;

	PlayerRoles _playerRoles;
	Movement _movement;

	void Awake()
	{
		_movement 		= GetComponent<Movement>();
		_playerRoles 	= GetComponent<PlayerRoles>();

		playerNum = GameMagager.Instance.GetPlayerId;
		//print(gameObject.name + " " + playerNum.ToString());
	}
}