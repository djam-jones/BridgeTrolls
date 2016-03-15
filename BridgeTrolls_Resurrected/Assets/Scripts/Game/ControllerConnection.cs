using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ControllerConnection : MonoBehaviour, IControllerConnect<bool> {

	[HideInInspector]
	public bool controllersConnected;
	private int _controllerAmount = 0;

	public int maxPlayers = 8;

	public static ControllerConnection Instance {get; private set;}

	public ControllerConnection(bool hasConnection)
	{
		CheckConnection(hasConnection);
		controllersConnected = hasConnection;
	}

	void Awake()
	{
		CheckForInstance();

		if(Input.GetJoystickNames().Length > 0)
		{
			print(Input.GetJoystickNames().Length + " Controller(s) is Connected!");
			CheckConnection(true);
		}
		else
		{
			print("No Controller Connected.");
			CheckConnection(false);
		}
	}

	void Update()
	{
		if(controllersConnected)
		{
			while( controllersConnected && _controllerAmount < maxPlayers )
			{
				if(Input.GetAxis("Horizontal_P" + _controllerAmount) > 0.1f || Input.GetAxis("Vertical_P" + _controllerAmount) > 0.1f)
				{
					print(Input.GetJoystickNames()[_controllerAmount] + " moved");
				}

				_controllerAmount++;
			}
		}
	}

	public void CheckConnection(bool hasConnection)
	{
		controllersConnected = hasConnection;
	}

	private void CheckForInstance()
	{
		//Check if there are any conflicting instances
		if(Instance != null && Instance != this)
		{
			//If so Destroy those instances.
			Destroy(gameObject);
		}
		//Save Singleton instance.
		Instance = this;

		//DontDestroyOnLoad(gameObject);
	}
}