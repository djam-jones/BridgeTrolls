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

		playerType = "Typeless";
	}

	void Start()
	{
		_smokeObject = this.transform.GetChild(1).gameObject;
		_smokeObject.SetActive(false);
	}

	void Update()
	{
		SetSortingOrder();
		this.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
		_smokeObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
	}

	public void SetCharacter(string characterName)
	{
		playerType = characterName;
	}

	private void SetSortingOrder()
	{
		GetComponent<SpriteRenderer>().sortingOrder = ((int)transform.position.y * -1) + 8;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == Tags.PLAYER_TAG && this.gameObject.tag == Tags.MINION_TAG)
		{
			StartCoroutine(ForcePushbackByDash(this.gameObject));
		}
	}

	private IEnumerator ForcePushbackByDash(GameObject other)
	{
		if(GetComponent<PlayerRoles>().playerRoles == Roles.Minion && other.GetComponent<AbilityHandler>()._dashUp)
		{
			if(other.transform.position.y > this.transform.position.y)
			{
				//Force Up
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
				yield return new WaitForSeconds(0.05f);
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -500));
				yield break;
			}
			else if(other.transform.position.y < this.transform.position.y)
			{
				//Force Down
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -500));
				yield return new WaitForSeconds(0.05f);
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
				yield break;
			}
		}
	}

	public IEnumerator Poof()
	{
		_smokeObject.SetActive(true);
		yield return new WaitForSeconds(1.2f);
		_smokeObject.SetActive(false);
	}
}