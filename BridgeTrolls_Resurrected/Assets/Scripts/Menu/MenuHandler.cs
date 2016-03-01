using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum MenuState
{
	Pre, 
	Main, 
	CharSelect, 
	Settings, 
	Credits
}

public class MenuHandler : MonoBehaviour {

	//Canvases
	public GameObject mainMenu;
	public GameObject charSelect;

	//Panels
	public GameObject buttonsPanel;

	//Texts
	public Text title;
	public Text pressA;

	//Axes
	private const string SELECT_X_AXIS = "HorizontalMenu";
	private const string SELECT_Y_AXIS = "VerticalMenu";

	//Buttons
	private const string SELECT_BUTTON = "ContinueButton";
	private const string BACK_BUTTON = "BackButton";

	//Enum
	public MenuState menuState;
	private int _menuIndex = 0;

	void Update()
	{
		CheckMenuIndex(_menuIndex);
		PreState();
	}

	public void PreState()
	{
		if(Input.GetButtonDown(SELECT_BUTTON) && _menuIndex == 0)
		{
			_menuIndex = 1;
		}
	}

	public void MainState()
	{
		if(Input.GetButtonDown(BACK_BUTTON) && _menuIndex == 1)
		{
			_menuIndex = 0;
		}
	}

	public void PlayState()
	{
		_menuIndex = 2;
	}

	public void SettingsState()
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
			menuState = MenuState.Settings;
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
				title.enabled = true;
				pressA.enabled = true;
			break;

			case MenuState.Main:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(true);
				title.enabled = true;
				pressA.enabled = false;
			break;

			case MenuState.CharSelect:
				mainMenu.SetActive(false);
				charSelect.SetActive(true);
				buttonsPanel.SetActive(false);
				title.enabled = false;
				pressA.enabled = false;
			break;

			case MenuState.Settings:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				title.enabled = true;
				pressA.enabled = false;
			break;

			case MenuState.Credits:
				mainMenu.SetActive(true);
				charSelect.SetActive(false);
				buttonsPanel.SetActive(false);
				title.enabled = true;
				pressA.enabled = false;
			break;
		}
	}

}