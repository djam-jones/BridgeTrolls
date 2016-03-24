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

	void Update()
	{
		SetSortingOrder();
		this.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
	}

	public void SetCharacter(string characterName)
	{
		playerType = characterName;
	}

	private void SetSortingOrder()
	{
		GetComponent<SpriteRenderer>().sortingOrder = ((int)transform.position.y * -1) + 8;
	}
}