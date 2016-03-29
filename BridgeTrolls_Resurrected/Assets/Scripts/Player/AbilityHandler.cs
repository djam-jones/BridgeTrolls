﻿using UnityEngine;
using System.Collections;

public class AbilityHandler : MonoBehaviour {

    [SerializeField, HideInInspector] private string _actionKey_A = "Fire1_P";
    [SerializeField, HideInInspector] private string _actionKey_B = "Fire2_P";

	private bool _dashUp = true;
	private bool _inCooldown = false;

	private float _fadeSpeed = 3f;

	private Roles _role;
	private Movement _move;
	private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite Dash;
    [SerializeField] private Sprite Idle;

    void Awake()
    {
        _role = this.gameObject.GetComponent<PlayerRoles>().playerRoles;
        _move = this.gameObject.GetComponent<Movement>();
		//_spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

	void Update()
	{
		UseAbility();

		if(_inCooldown)
			Cooldown(transform.GetChild(1).GetComponent<SpriteRenderer>());
	}

    private void UseAbility()
    {
        if (Input.GetButtonDown(_actionKey_A + GetComponent<Player>().playerNum))
        {
            if (GetComponent<PlayerRoles>().playerRoles == Roles.Hostile)
            {
                Ability_Scratch();
            }
            else if (GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && GetComponent<PlayerRoles>().playerRoles != Roles.Hostile && _dashUp == true)
            {
                StartCoroutine(Ability_Dash());
            }
        }
        else if (Input.GetButtonDown(_actionKey_B + GetComponent<Player>().playerNum))
        {
            
            Debug.Log("WOOOW B");
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

        Debug.Log("dash");
        _move.speed += 7;
        //GetComponent<SpriteRenderer>().sprite = Dash;
        yield return new WaitForSeconds(0.1f);
        //GetComponent<SpriteRenderer>().sprite = Idle;
        _move.speed -= 7;
        _dashUp = false;
		_inCooldown = true;
        yield return new WaitForSeconds(3);
        _dashUp = true;
		_inCooldown = false;
//		_spriteRenderer.color = Color.white;
    }

    public void Ability_Scratch()
    {
        Debug.Log("scratch");
		//TODO: Show Animation.

		//Make a hitbox and use it with the animation.
		//If a goblin player is in the hitbox,
		//Do something...
    }

	public void Cooldown(SpriteRenderer renderer)
	{
		renderer.color = Color.Lerp(Color.white, new Color(0.52f, 0.318f, 1f), Mathf.PingPong(Time.time * _fadeSpeed, 1.0f));
	}

}