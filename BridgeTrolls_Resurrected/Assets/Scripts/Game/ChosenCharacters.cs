using UnityEngine;
using System.Collections;

public class ChosenCharacters : MonoBehaviour {

	public const string playerOne = "";
	public const string playerTwo = "";
	public const string playerThree = "";
	public const string playerFour = "";
	public const string playerFive = "";
	public const string playerSix = "";
	public const string playerSeven = "";
	public const string playerEight = "";

	public static ChosenCharacters Instance {get; private set;}

	void Awake()
	{
		CheckForInstance();
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

		DontDestroyOnLoad(gameObject);
	}
}