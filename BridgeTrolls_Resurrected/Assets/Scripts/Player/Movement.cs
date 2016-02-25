using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public bool devKeyBoardInput = false;

	[SerializeField, HideInInspector] private string _horizontalControl  = "Horizontal_P";
	[SerializeField, HideInInspector] private string _verticalControl 	= "Vertical_P";

	[SerializeField] private float _speed;

	[SerializeField] private float _minClampedX;
	[SerializeField] private float _maxClampedX;

	[SerializeField] private float _minClampedY;
	[SerializeField] private float _maxClampedY;

	private float _clampOffset = 7;

	private PlayerRoles _playerRolesScript;

	void Awake()
	{
		_playerRolesScript = GetComponent<PlayerRoles>();
	}

	void FixedUpdate()
	{
		if(_playerRolesScript.playerRoles == Roles.Neutral)
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
			h = Input.GetAxis( _horizontalControl + GetComponent<Player>().playerNum ) * _speed * Time.deltaTime;
			v = Input.GetAxis( _verticalControl   + GetComponent<Player>().playerNum ) * _speed * Time.deltaTime;
		}
		else 
		{
			h = Input.GetAxis( "Horizontal" ) * _speed * Time.deltaTime;
			v = Input.GetAxis( "Vertical" ) * _speed * Time.deltaTime;
		}
		transform.Translate(new Vector2(h, v));
	}

	private void Move()
	{
		float h;
		float v;

		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

		pos.x = Mathf.Clamp(pos.x, _clampOffset, Screen.width - _clampOffset);
		pos.y = Mathf.Clamp(pos.y, _clampOffset, Screen.height - _clampOffset);
		pos.z = (transform.position.z + 10);

		transform.position = Camera.main.ScreenToWorldPoint(pos);

		if(!devKeyBoardInput)
		{
			h = Input.GetAxis( _horizontalControl + GetComponent<Player>().playerNum ) * _speed * Time.deltaTime;
			v = Input.GetAxis( _verticalControl   + GetComponent<Player>().playerNum ) * _speed * Time.deltaTime;
		}
		else
		{
			h = Input.GetAxis( "Horizontal" ) * _speed * Time.deltaTime;
			v = Input.GetAxis( "Vertical" ) * _speed * Time.deltaTime;
		}
		transform.Translate(new Vector2(h, v));
	}
}