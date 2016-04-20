using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

	[SerializeField, HideInInspector] private const string _actionKey = "ContinueButton";

	public GameObject tutorialScreen;

	private bool _showThis;

	StartScreenHandler _cDown;
	Animator _anim;

	void Awake()
	{
		_anim = tutorialScreen.GetComponent<Animator>();
		
		_cDown = GetComponent<StartScreenHandler>();
		_cDown.enabled = false;

		if(PlayerPrefs.GetString("ShowTutorial") == "True" && PlayerPrefs.HasKey("ShowTutorial"))
			_showThis = true;
		else if(PlayerPrefs.GetString("ShowTutorial") == "False" && PlayerPrefs.HasKey("ShowTutorial"))
			_showThis = false;

		if(_showThis)
		{
			Continue();
		}
		else if(!_showThis)
		{
			tutorialScreen.SetActive(false);
			_cDown.enabled = true;
		}
	}

	private void Continue()
	{
		if(Input.GetButtonDown(_actionKey))
		{
			print("hello");
			_anim.SetTrigger("Hide");
			StartCoroutine(Wait(1.25f));
		}
	}

	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		tutorialScreen.SetActive(false);
		_cDown.enabled = true;
		yield break;
	}
}