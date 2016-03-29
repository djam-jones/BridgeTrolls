using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int playerNum;
	public string playerType;

	GameObject _smokeObject;

	PlayerRoles _playerRoles;
	Movement _movement;

	void Awake()
	{
		_movement 		= GetComponent<Movement>();
		_playerRoles 	= GetComponent<PlayerRoles>();

		_smokeObject = transform.GetChild(2);

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

	public IEnumerator Poof()
	{
		_smokeObject.SetActive(true);
		yield return new WaitForSeconds(1.2f);
		_smokeObject.SetActive(false);
	}
}