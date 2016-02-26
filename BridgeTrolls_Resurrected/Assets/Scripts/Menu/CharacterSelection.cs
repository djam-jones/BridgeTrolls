using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

	[SerializeField, HideInInspector] private string _dPadControls = "D-Pad";

	public GameObject[] playableCharacters = new GameObject[2];
	private GameObject _characterHolder;

	void Awake()
	{
		_characterHolder = GameObject.Find("Characters");
	}

	public void Select()
	{
		if(Input.GetAxis(_dPadControls + GetComponent<HubHandler>().panelID) < 0.1)
		{
			print("right");
		}
		else if(Input.GetAxis(_dPadControls + GetComponent<HubHandler>().panelID) > 0.1)
		{
			print("left");
		}
	}

	public void Confirmed()
	{

	}

}