using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int playerNum;
	public string playerType;

	GameObject _effectObject;

	PlayerRoles _playerRoles;
	Movement _movement;

	void Awake()
	{
		_movement 		= GetComponent<Movement>();
		_playerRoles 	= GetComponent<PlayerRoles>();

		playerType = "Typeless";
        SetControllerID();
	}
    
	void Start()
	{
		_effectObject = this.transform.GetChild(1).gameObject;
		_effectObject.SetActive(false);
	}

	void Update()
	{
		SetSortingOrder();
		this.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
		_effectObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
	}

    public void SetControllerID()
    {
        playerNum = ControllerAssigner.Instance.ControllerIndex;
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
		if(GetComponent<PlayerRoles>().playerRoles == Roles.Minion && other.GetComponent<AbilityHandler>().dashUp == true)
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
		_effectObject.SetActive(true);
		_effectObject.GetComponent<Animator>().Play("Poof");
		yield return new WaitForSeconds(1.2f);
		_effectObject.SetActive(false);
	}

	public IEnumerator DashPoof()
	{
		_effectObject.SetActive(true);
		_effectObject.GetComponent<Animator>().Play("DashEffect_Animation");
		yield return new WaitForSeconds(1f);
		_effectObject.SetActive(false);
		yield return null;
	}

	public IEnumerator RunPoof()
	{
		_effectObject.SetActive(true);
		_effectObject.GetComponent<Animator>().Play("RunEffect_Animation");
		yield return new WaitForSeconds(1f);
		_effectObject.SetActive(false);
		yield return null;
	}
}