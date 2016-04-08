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

	[SerializeField] private float _minClampedY = -6.5f;
	[SerializeField] private float _maxClampedY = 6.5f;

	private Player _player;
	private PlayerRoles _playerRolesScript;
	private Rigidbody2D _rigidbody2D;
	private Animator 	_anim;

	public bool facingRight = false;
	private bool _isMoving = false;

	void Awake()
	{
		_player = GetComponent<Player>();
		_playerRolesScript = GetComponent<PlayerRoles>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_scale = transform.localScale.x;
	}

	void FixedUpdate()
	{
		if(_playerRolesScript.playerRoles == Roles.Neutral)
			Move(-11.6f, 11.6f);
		else if(_playerRolesScript.playerRoles == Roles.Minion)
			Move(-7.55f, 7.55f);
		else if(_playerRolesScript.playerRoles == Roles.Hostile)
			ClampedMove();

		if(_isMoving) {
			_anim.SetTrigger("Go_Run");
		}
		else {
			_anim.SetTrigger("Go_Idle");
		}
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
//			transform.localScale = new Vector2(-_scale, transform.localScale.y);
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
		}
		else if(h <= -0.01f)
		{
//			transform.localScale = new Vector2(_scale, transform.localScale.y);
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
		}

		//Checks the Moving bool
		CheckMoving(h, v);

		transform.Translate(new Vector2(h, v), Space.World);
	}

	private void Move(float clampMin, float clampMax)
	{
		float h;
		float v;

		Vector2 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x, clampMin, clampMax);
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
			//			transform.localScale = new Vector2(-_scale, transform.localScale.y);
			gameObject.GetComponent<SpriteRenderer>().flipX = true;
		}
		else if(h <= -0.01f)
		{
			//			transform.localScale = new Vector2(_scale, transform.localScale.y);
			gameObject.GetComponent<SpriteRenderer>().flipX = false;
		}

		//Checks the Moving bool
		CheckMoving(h, v);

		transform.Translate(new Vector2(h, v), Space.World);
	}

	/// <summary>
	/// Changes the isMoving Bool, depending on the H float value.
	/// </summary>
	/// <param name="movingFactor">Moving factor.</param>
	private void CheckMoving(float movingFactorX, float movingFactorY)
	{
		if(movingFactorX >= 0.01f || movingFactorX <= -0.01f || movingFactorY >= 0.01f || movingFactorY <= -0.01f)
			_isMoving = true;
		else
			_isMoving = false;
	}
}