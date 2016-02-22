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
}
