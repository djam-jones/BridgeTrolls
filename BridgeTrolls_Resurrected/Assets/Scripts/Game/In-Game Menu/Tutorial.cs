using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

	[SerializeField, HideInInspector] private const string _actionKey = "ContinueButton";

	public GameObject tutorialScreen;

	StartScreenHandler _cDown;
	Animator _anim;

	void Awake()
	{
		_anim = tutorialScreen.GetComponent<Animator>();

		_cDown = GetComponent<StartScreenHandler>();
		_cDown.enabled = false;
	}

	void Update()
	{
		Continue();
	}

	private void Continue()
	{
		if(Input.GetButtonDown(_actionKey))
		{
			_anim.SetTrigger("Hide");
			StartCoroutine(Wait(1.5f));
		}
	}

	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		tutorialScreen.SetActive(false);
		_cDown.enabled = true;
	}
}