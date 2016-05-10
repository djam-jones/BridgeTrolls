using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public enum MenuState
{
	Pre, 
	Main, 
	CharSelect, 
	Options, 
	Credits, 
	InQuit
}

public class MenuHandler : MonoBehaviour {

	//Canvases
	public GameObject mainMenu;
	public GameObject charSelect;
	public GameObject optionsMenu;
	public GameObject creditsScreen;

	//Event System
	public GameObject eventSystem;

	//Panels
	public GameObject buttonsPanel;
	public GameObject popUpQuitBox;

	//Texts
	public Image title;
	public Text pressA;

	//Buttons
	public GameObject leftSelectionArrow;
	public GameObject rightSelectionArrow;
	public GameObject playButton;

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

	void Awake()
	{
		_openAnimator = backDoor.GetComponent<Animator>();
		popUpQuitBox.SetActive(false);
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

			eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = playButton;
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject( playButton );
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
			_openAnimator.Play("Open");

			if(_timer >= _buttonTime) //If timer is greater than, or equals the _buttonTime value.
			{
				_menuIndex = 1; //Set the Menu Index value to 1.
				_timer = 0; //Reset The Timer.
			}
		}
		if(Input.GetButtonUp(BACK_BUTTON) && menuState == MenuState.CharSelect)
		{
//			_openAnimator.SetTrigger("Close"); //Play Closing Animation.
			_openAnimator.Play("Close");

			_timer = 0f; //Reset the Timer.
		}
		else if(Input.GetButton(BACK_BUTTON) && menuState == MenuState.Credits)
		{
			_menuIndex = 1;

			eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = playButton;
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject( playButton );
		}
		else if(Input.GetButton(BACK_BUTTON) && menuState == MenuState.Options)
		{
			_menuIndex = 1;

			eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = playButton;
			eventSystem.GetComponent<EventSystem>().SetSelectedGameObject( playButton );
		}
	}

	public void PlayState()
	{
		_menuIndex = 2;
	}

	public void OptionsState()
	{
		_menuIndex = 3;

		eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = optionsMenu.transform.GetChild(3).gameObject;
		eventSystem.GetComponent<EventSystem>().SetSelectedGameObject( optionsMenu.transform.GetChild(3).gameObject );
	}

	public void CreditsState()
	{
		_menuIndex = 4;
	}

	public void QuitGame()
	{
		print("QUITTING...");
		Application.Quit();
	}

	public void PopUpQuitBox(GameObject button)
	{
		_menuIndex = 5;

		eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = button;
		eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(button, null);
	}

	public void HideQuitBox(GameObject button)
	{
		_menuIndex = 1;

		eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = button;
		eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(button, null);
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
		case 5:
			menuState = MenuState.InQuit;
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
				popUpQuitBox.SetActive(false);

				title.enabled = true;
				pressA.enabled = true;
			break;

			case MenuState.Main:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(true);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(false);
				popUpQuitBox.SetActive(false);

				title.enabled = true;
				pressA.enabled = false;
			break;

			case MenuState.CharSelect:
				mainMenu.SetActive(false);
				charSelect.SetActive(true);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(false);
				popUpQuitBox.SetActive(false);

				title.enabled = false;
				pressA.enabled = false;
			break;

			case MenuState.Options:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(true);
				creditsScreen.SetActive(false);
				popUpQuitBox.SetActive(false);

				title.enabled = true;
				pressA.enabled = false;
			break;

			case MenuState.Credits:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(true);
				popUpQuitBox.SetActive(false);

				title.enabled = true;
				pressA.enabled = false;
			break;

			case MenuState.InQuit:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				optionsMenu.SetActive(false);
				creditsScreen.SetActive(false);
				popUpQuitBox.SetActive(true);

				title.enabled = true;
				pressA.enabled = false;
			break;
		}
	}

}