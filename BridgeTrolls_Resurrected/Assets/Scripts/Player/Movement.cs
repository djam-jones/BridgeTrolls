using UnityEngine;
using System.Collections;

public enum Roles
{
	Hostile, 
	Neutral
}

public class Movement : MonoBehaviour {

	private Roles _playerRoles;

	[SerializeField]
	private float _speed;

	void Update()
	{
		Move();
	}

	private void Move()
	{
		float h = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
		float v = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

		transform.Translate(new Vector2(h, v));
	}
}