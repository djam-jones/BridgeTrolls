using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerAssigner : MonoBehaviour 
{
	[SerializeField, HideInInspector] private const string _actionKey = "Submit";
	[SerializeField, HideInInspector] private const string _cancelKey = "Cancel";

	[SerializeField] private string[] _controllerButtonNameArray = new string[8];
	[SerializeField] public List<string> allControllersInOrder = new List<string>();
	[SerializeField] private int _controllerIndex = 0;

	//Instance
	public static ControllerAssigner Instance {get; private set;}

	void Awake()
	{
		CheckForInstance();
		allControllersInOrder.Capacity = 8;
	}

	void Update()
	{
		for(int i = 0; i < _controllerButtonNameArray.Length; i++)
		{
			if(Input.GetButtonDown(_controllerButtonNameArray[i]))
			{
				print(_controllerButtonNameArray[i]);
				allControllersInOrder.Add(_controllerButtonNameArray[i]);
			}
		}
	}

	public int ControllerIndex
	{
		get
		{
			for(int i = 0; i < allControllersInOrder.Count; i++)
			{
				_controllerIndex = i;
			}
			return _controllerIndex;
		}
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