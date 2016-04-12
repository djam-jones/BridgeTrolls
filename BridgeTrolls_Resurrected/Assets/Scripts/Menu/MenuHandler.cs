﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public enum MenuState
{
	Pre, 
	Main, 
	CharSelect, 
	Options, 
	Credits
}

public class MenuHandler : MonoBehaviour {

	//Canvases
	public GameObject mainMenu;
	public GameObject charSelect;
	public GameObject optionsMenu;
	public GameObject creditsScreen;

	//Panels
	public GameObject buttonsPanel;

	//Texts
	public Image title;
	public Text pressA;

	//BackDoor
	public GameObject backDoor;

	//Axes
	private const string SELECT_X_AXIS = "HorizontalMenu";
	private const string SELECT_Y_AXIS = "VerticalMenu";

	//Buttons
	private const string SELECT_BUTTON = "ContinueButton";
	private const string BACK_BUTTON = "BackButton";

	//Enum
	public MenuState menuState;
	private int _menuIndex = 0;

	//Floats
	private float _timer;
	private float _buttonTime = 1.5f;

	Animator _openAnimator;
	Animation _doorAnimations;

	void Awake()
	{
		_openAnimator = backDoor.GetComponent<Animator>();
		_doorAnimations = backDoor.GetComponent<Animation>();
	}

	void Update()
	{
		CheckMenuIndex(_menuIndex);
		PreState();
		MainState();
		BackState();
	}

	public void PreState()
	{
		if(Input.GetButtonDown(SELECT_BUTTON) && menuState == MenuState.Pre)
		{
			_menuIndex = 1;
			title.GetComponent<Animator>().SetTrigger("Up");
		}
	}

	public void MainState()
	{
		if(Input.GetButtonDown(BACK_BUTTON) && menuState == MenuState.Main)
		{
			_menuIndex = 0;
			title.GetComponent<Animator>().SetTrigger("Down");
		}
	}

	public void BackState()
	{
		if(Input.GetButton(BACK_BUTTON) && menuState == MenuState.CharSelect)
		{
			_timer += Time.deltaTime; //Count seconds up to the Timer.

//			_openAnimator.SetTrigger("Open"); //Play Opening Animation.
			_doorAnimations.Play("Open");

			if(_timer >= _buttonTime) //If timer is greater than, or equals the _buttonTime value.
			{
				_menuIndex = 1; //Set the Menu Index value to 1.
				_timer = 0; //Reset The Timer.
			}
		}
		if(Input.GetButtonUp(BACK_BUTTON) && menuState == MenuState.CharSelect)
		{
//			_openAnimator.SetTrigger("Close"); //Play Closing Animation.
			_doorAnimations.Play("Close");

			_timer = 0f; //Reset the Timer.
		}
		else if(Input.GetButton(BACK_BUTTON) && menuState == MenuState.Credits)
		{
			_menuIndex = 1;
		}
		else if(Input.GetButton(BACK_BUTTON) && menuState == MenuState.Options)
		{
			_menuIndex = 1;
		}
	}

	public void PlayState()
	{
		_menuIndex = 2;
	}

	public void OptionsState()
	{
		_menuIndex = 3;
	}

	public void CreditsState()
	{
		_menuIndex = 4;
	}

	public void QuitGame()
	{
		print("Quitting Game...");
		Application.Quit();
	}


	private void CheckMenuIndex(int index)
	{
		switch(index)
		{
		case 0:
			menuState = MenuState.Pre;
			break;
		case 1:
			menuState = MenuState.Main;
			break;
		case 2:
			menuState = MenuState.CharSelect;
			break;
		case 3:
			menuState = MenuState.Options;
			break;
		case 4:
			menuState = MenuState.Credits;
			break;
		}
		SetMenuState();
	}

	private void SetMenuState()
	{
		switch(menuState)
		{
			case MenuState.Pre:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(false);
				title.enabled = true;
				pressA.enabled = true;
			break;

			case MenuState.Main:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(true);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(false);
				title.enabled = true;
				pressA.enabled = false;
			break;

			case MenuState.CharSelect:
				mainMenu.SetActive(false);
				charSelect.SetActive(true);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(false);
				title.enabled = false;
				pressA.enabled = false;
			break;

			case MenuState.Options:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(true);
				creditsScreen.SetActive(false);
				title.enabled = true;
				pressA.enabled = false;
			break;

			case MenuState.Credits:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(true);
				title.enabled = true;
				pressA.enabled = false;
			break;
		}
	}

}