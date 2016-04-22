using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PunManager : MonoBehaviour {

	//Array for puns.
	public string[] trollPuns = new string[23];

	//Text for pun usage.
	public Text punText;

	void Awake()
	{
		RandomQuote();
	}

	void LateUpdate()
	{
		if(Input.GetKeyDown(KeyCode.F5))
		{
			RandomQuote();
		}
	}

	private void RandomQuote()
	{
		punText.text = trollPuns[ Random.Range(0, trollPuns.Length) ];
	}

}