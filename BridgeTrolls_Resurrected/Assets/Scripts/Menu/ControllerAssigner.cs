using UnityEngine;
using System.Collections;

public class ControllerAssigner : MonoBehaviour 
{
	[SerializeField, HideInInspector] private const string _actionKey = "Submit";
	[SerializeField, HideInInspector] private const string _cancelKey = "Cancel";

	[SerializeField] private string[] controllerButtonNameArray = new string[8];

	void Update()
	{
		for(int i = 0; i < controllerButtonNameArray.Length; i++)
		{
			if(Input.GetButtonDown(controllerButtonNameArray[i]))
			{
				print(i);
			}
		}
	}

}