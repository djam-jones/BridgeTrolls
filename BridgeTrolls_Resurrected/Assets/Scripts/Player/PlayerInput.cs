using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public delegate void KeyPressed();

	public event KeyPressed UpKeyPressed;
	public event KeyPressed DownKeyPressed;
	public event KeyPressed LeftKeyPressed;
	public event KeyPressed RightKeyPressed;
	public event KeyPressed ActionKeyPressed;
	public event KeyPressed NoKeyPressed;

	private string _horAxis;
	private string _vertAxis;
	private string _actionKey;

	PlayerInputHandler playerInputHandler;

	void Start()
	{
		playerInputHandler = GetComponent<PlayerInputHandler>();

	}

	void Update()
	{
		Inputs();
	}

	private void Inputs()
	{
		bool keyIsPressed = false;

		//Send Events for moving LEFT & RIGHT
		if(Input.GetAxis(_horAxis) > 0) //Send Right Event
		{
			keyIsPressed = true;
			if(RightKeyPressed != null)
				RightKeyPressed();
		}
		else if(Input.GetAxis(_horAxis) < 0) //Send Left Event
		{
			keyIsPressed = true;
			if(LeftKeyPressed != null)
				LeftKeyPressed();
		}

		//Send Events for moving UP & DOWN
		if(Input.GetAxis(_vertAxis) > 0) //Send Up Event
		{
			keyIsPressed = true;
			if(UpKeyPressed != null)
				UpKeyPressed();
		}
		else if(Input.GetAxis(_vertAxis) < 0) //Send Down Event
		{
			keyIsPressed = true;
			if(DownKeyPressed != null)
				DownKeyPressed();
		}

		//Send Event for the ACTION Key
		if(Input.GetButtonDown(_actionKey)) //Send Action Event
		{
			keyIsPressed = true;
			if(ActionKeyPressed != null)
				ActionKeyPressed();
		}

		if(!keyIsPressed)
			if(NoKeyPressed != null)
				NoKeyPressed();

	}

}