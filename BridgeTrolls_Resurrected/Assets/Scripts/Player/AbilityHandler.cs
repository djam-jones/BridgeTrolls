using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityHandler : MonoBehaviour
{

	[SerializeField, HideInInspector]
	private string _actionKey_A = "Fire1_P";
	[SerializeField, HideInInspector]
	private string _actionKey_B = "Fire2_P";

	public bool dashUp = true;
	public bool scratchAvailable = true;
	private bool _inCooldown = false;
	private bool _isRawring = false;
	private bool _inScratch = false;

	private float _fadeSpeed = 3f;

	private Player _player;
	private Roles _role;
	private Movement _move;
	private GameMagager manager;
	private SpriteRenderer _spriteRenderer;
	private Animator _anim;

	[SerializeField]
	private Sprite Dash;
	[SerializeField]
	private Sprite Idle;

	void Awake()
	{
		manager = GameObject.Find("GameHandeler").GetComponent<GameMagager>();

		_role = this.gameObject.GetComponent<PlayerRoles>().playerRoles;
		_move = this.gameObject.GetComponent<Movement>();
	}

	void Start()
	{
		_spriteRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
		_anim = GetComponent<Animator>();
	}

	void Update()
	{
		UseAbility();

		if (_inCooldown)
			Cooldown(_spriteRenderer);

		if (_isRawring)
			StartCoroutine(Ability_Roar());
	}

	private void UseAbility()
	{
		if (Input.GetButtonDown(_actionKey_A + GetComponent<Player>().playerNum))
		{
			if (GetComponent<PlayerRoles>().playerRoles == Roles.Hostile && scratchAvailable == true)
			{
				StartCoroutine(Ability_Scratch());
			}
			else if (GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && GetComponent<PlayerRoles>().playerRoles != Roles.Hostile && dashUp == true)
			{
				StartCoroutine(Ability_Dash());
			}
			else if(GetComponent<PlayerRoles>().playerRoles == Roles.Minion && GetComponent<Grab>().grabReady == true)
			{
				StartCoroutine(Ability_Grab());
			}
		}
		else if (Input.GetButtonDown(_actionKey_B + GetComponent<Player>().playerNum))
		{
			if (GetComponent<PlayerRoles>().playerRoles == Roles.Hostile && _inCooldown == false)
			{
				_isRawring = true;
			}
		}
	}

	IEnumerator Ability_Dash()
	{
		/*Debug.Log("dash");
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(200,0));
        DashUp = false;
        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(new Vector2(-200, 0));
        yield return new WaitForSeconds(3f);
        DashUp = true;*/

		//        Debug.Log("dash");
		_move.speed += 7;
		_anim.SetTrigger("Go_Dash");
		yield return new WaitForSeconds(0.1f);
		_move.speed -= 7;
		dashUp = false;
		_inCooldown = true;
		yield return new WaitForSeconds(3);
		dashUp = true;
		_inCooldown = false;
		_spriteRenderer.color = Color.white;
	}

	IEnumerator Ability_Grab()
	{
		Grab grabScript = GetComponent<Grab>();
		_move.speed += 6;
		yield return new WaitForSeconds(0.1f);
		_move.speed -= 6;
		grabScript.grabReady = false;
		_inCooldown = true;
		yield return new WaitForSeconds(3);
		grabScript.grabReady = true;
		_inCooldown = false;
		_spriteRenderer.color = Color.white;
	}

	public IEnumerator Ability_Scratch()
	{
		//        Debug.Log("scratch");
		//Play Animation.
		_anim.SetTrigger("Go_Grab");
		_inScratch = true;
		yield return new WaitForSeconds(0.1f);
		_inScratch = false;
		scratchAvailable = false;
		_inCooldown = true;
		yield return new WaitForSeconds(3);
		scratchAvailable = true;
		_inCooldown = false;
		_spriteRenderer.color = Color.white;
	}

	IEnumerator Ability_Roar()
	{
		//play animation
		_anim.SetTrigger("Go_Roar");

		List<GameObject> goblin = manager.allGoblins;
		for (int i = 0; i < goblin.Count; i++)
		{
			Vector2 startPosition = goblin[i].transform.position;
			Vector2 newPosition = Random.insideUnitCircle * 3f;
			newPosition.x += goblin[i].transform.position.x;
			newPosition.y += goblin[i].transform.position.y;
			goblin[i].transform.position = Vector3.Lerp(startPosition, newPosition, Time.deltaTime * 7f);
		}
		yield return new WaitForSeconds(0.25f);
		_isRawring = false;
		_inCooldown = true;
		yield return new WaitForSeconds(7f);
		_inCooldown = false;
		_spriteRenderer.color = Color.white;
		yield return null;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == Tags.PLAYER_TAG && _inScratch)
		{
			other.GetComponent<SwitchTeam>().Switch();
		}
	}

	public void Cooldown(SpriteRenderer renderer)
	{
		renderer.color = Color.Lerp(Color.white, new Color(0.52f, 0.318f, 1f), Mathf.PingPong(Time.time * _fadeSpeed, 1.0f));
	}

}