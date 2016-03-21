using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int playerNum;
	public string playerType;

	PlayerRoles _playerRoles;
	Movement _movement;

	void Awake()
	{
		_movement 		= GetComponent<Movement>();
		_playerRoles 	= GetComponent<PlayerRoles>();

		playerType = "Typeless";
	}

	public void SetCharacter(string characterName)
	{
		playerType = characterName;
	}
}