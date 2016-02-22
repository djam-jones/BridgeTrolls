using UnityEngine;
using System.Collections;

public enum Roles
{
	Hostile, 
	Neutral
}

public class Movement : MonoBehaviour {

	public Roles playerRoles;

	[SerializeField] private float _speed;

	[SerializeField] private float _minClampedX;
	[SerializeField] private float _maxClampedX;

	[SerializeField] private float _minClampedY;
	[SerializeField] private float _maxClampedY;

	public static Movement Instance {get; private set;}

	void FixedUpdate()
	{
		if(playerRoles == Roles.Neutral)
			Move();
		else if(playerRoles == Roles.Hostile)
			ClampedMove();
	}

	private void ClampedMove()
	{
		float h = Input.GetAxis("Horizontal") 	* _speed * Time.deltaTime;
		float v = Input.GetAxis("Vertical") 	* _speed * Time.deltaTime;

		Vector2 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, _minClampedX, _maxClampedX);
		pos.y = Mathf.Clamp(pos.y, _minClampedY, _maxClampedY);
		transform.position = pos;

		transform.Translate(new Vector2(h, v));
	}

	private void Move()
	{
		float h = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
		float v = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

		transform.Translate(new Vector2(h, v));
	}
}