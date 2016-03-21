using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public bool devKeyBoardInput = false;

	[SerializeField, HideInInspector] private string _horizontalControl  = "Horizontal_P";
	[SerializeField, HideInInspector] private string _verticalControl 	= "Vertical_P";

	public float speed;
	[SerializeField, HideInInspector] private float _scale;

	[SerializeField] private float _minClampedX = -1.6f;
	[SerializeField] private float _maxClampedX = 1.6f;

	[SerializeField] private float _minClampedY = -6f;
	[SerializeField] private float _maxClampedY = 6f;

	private float _clampOffset = 7;

	private PlayerRoles _playerRolesScript;
	private Rigidbody2D _rigidbody2D;

	public bool facingRight;

	void Awake()
	{
		_playerRolesScript = GetComponent<PlayerRoles>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_scale = transform.localScale.x;
	}

	void FixedUpdate()
	{
		if(_playerRolesScript.playerRoles == Roles.Neutral)
			Move();
		else if(_playerRolesScript.playerRoles == Roles.Minion)
			Move();
		else if(_playerRolesScript.playerRoles == Roles.Hostile)
			ClampedMove();
	}

	private void ClampedMove()
	{
		float h;
		float v;

		Vector2 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, _minClampedX, _maxClampedX);
			pos.y = Mathf.Clamp(pos.y, _minClampedY, _maxClampedY);
		transform.position = pos;

		if(!devKeyBoardInput)
		{
			h = Input.GetAxis( _horizontalControl + GetComponent<Player>().playerNum ) * speed * Time.deltaTime;
			v = Input.GetAxis( _verticalControl   + GetComponent<Player>().playerNum ) * speed * Time.deltaTime;
		}
		else 
		{
			h = Input.GetAxis( "Horizontal" ) * speed * Time.deltaTime;
			v = Input.GetAxis( "Vertical" ) * speed * Time.deltaTime;
		}

		if(h >= 0.01f)
		{
//			transform.localScale = new Vector2(_scale, transform.localScale.y);
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
		}
		else if(h <= -0.01f)
		{
//			transform.localScale = new Vector2(-_scale, transform.localScale.y);
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
		}

		transform.Translate(new Vector2(h, v), Space.World);
	}

	private void Move()
	{
		float h;
		float v;

		Vector2 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, -11.5f, 11.5f);
			pos.y = Mathf.Clamp(pos.y, _minClampedY, _maxClampedY);
		transform.position = pos;

		if(!devKeyBoardInput)
		{
			h = Input.GetAxis( _horizontalControl + GetComponent<Player>().playerNum ) * speed * Time.deltaTime;
			v = Input.GetAxis( _verticalControl   + GetComponent<Player>().playerNum ) * speed * Time.deltaTime;
		}
		else
		{
			h = Input.GetAxis( "Horizontal" ) * speed * Time.deltaTime;
			v = Input.GetAxis( "Vertical" ) * speed * Time.deltaTime;
		}

		if(h >= 0.01f)
		{
			transform.localScale = new Vector2(_scale, transform.localScale.y);
		}
		else if(h <= -0.01f)
		{
			transform.localScale = new Vector2(-_scale, transform.localScale.y);
		}

		transform.Translate(new Vector2(h, v), Space.World);
	}
}