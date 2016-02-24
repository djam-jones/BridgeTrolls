using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

	private Movement movement;

	//Input
	private PlayerInput _playerInput;
	private string _horAxis = "Horizontal_P0";
	private string _vertAxis = "Vertical_P0";
	private string _actionKey = "Fire1_P0";

	void Awake()
	{
		_playerInput = gameObject.AddComponent<PlayerInput>();
		movement = gameObject.GetComponent<Movement>();
	}

	void Start()
	{

	}
}
