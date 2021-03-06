﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum PanelState
{
	WaitingToJoin, 
	CharacterSelect, 
	Ready
}

public enum PlayerState
{
	Unready, 
	Ready
}

public class HubHandler : MonoBehaviour {

	[SerializeField, HideInInspector] private string _actionKey = "Submit";
	[SerializeField, HideInInspector] private string _cancelKey = "Cancel";

	public int panelID;
	[HideInInspector] public PanelState panelState;
	[HideInInspector] public PlayerState playerState;
	[SerializeField, HideInInspector] private int _panelIndex = 0;

	public int totalCharacters = 2;

	[SerializeField] private Text[] _panelTexts = new Text[2];
	[SerializeField] private Image[] _panelImages = new Image[6];

	private CharacterSelection _characterSelection;
    private AudioHandler _audioHandler;

    void Awake()
	{
		_characterSelection = GetComponent<CharacterSelection>();
        _audioHandler = GameObject.Find("Audio Handler").GetComponent<AudioHandler>();
        panelState = PanelState.WaitingToJoin;
		playerState = PlayerState.Unready;

        //SetPanelID();
	}

	void Update()
	{
		CheckState(_panelIndex);

		CheckReady();
		CheckInCharacterSelect();

		ProgressState();
		RegressState();
	}

	private void SetPanelID()
	{
		panelID = ControllerAssigner.Instance.ControllerIndex;
	}

	private void CheckState(int panelIndex)
	{
		switch(panelIndex)
		{
		case 0:
			panelState = PanelState.WaitingToJoin;
			playerState = PlayerState.Unready;

			//Disabled Joystick/D-Pad controls.

			_panelTexts[0].enabled = false;
			_panelTexts[1].enabled = false;

			_panelImages[0].enabled = true;
			_panelImages[1].enabled = true;
			_panelImages[2].enabled = true;
			_panelImages[3].enabled = false;
			_panelImages[4].enabled = false;
			_panelImages[5].gameObject.SetActive(false);
			break;
		case 1:
			panelState = PanelState.CharacterSelect;
			playerState = PlayerState.Unready;

			//Enable Joystick/D-Pad controls.
//				_characterSelection.Select();

			_panelTexts[0].enabled = false;
			_panelTexts[1].enabled = false;

			_panelImages[0].enabled = true;
			_panelImages[1].enabled = false;
			_panelImages[2].enabled = false;
			_panelImages[3].enabled = true;
			_panelImages[4].enabled = true;
			_panelImages[5].gameObject.SetActive(true);
			break;
		case 2:
			panelState = PanelState.Ready;
			playerState = PlayerState.Ready;

            _characterSelection.Confirm();

			_panelTexts[0].text = "Player " + (panelID + 1).ToString();

			_panelTexts[0].enabled = true;
			_panelTexts[1].enabled = true;

			_panelImages[0].enabled = false;
			_panelImages[1].enabled = false;
			_panelImages[2].enabled = false;
			_panelImages[3].enabled = false;
			_panelImages[4].enabled = false;
			_panelImages[5].gameObject.SetActive(false);
			break;
		}
	}

	private void ProgressState()
	{
		if(Input.GetButtonDown(_actionKey + panelID.ToString()))
		{
			if(_panelIndex >= 0 && _panelIndex < totalCharacters)
			{
				_panelIndex++;
			}
		}
	}

	private void RegressState()
	{
		if(Input.GetButtonDown(_cancelKey + panelID.ToString()))
		{
			if(_panelIndex > 0 && _panelIndex <= totalCharacters)
			{
				_panelIndex--;
			}
		}
	}

	public void CheckReady()
	{
		if(playerState == PlayerState.Ready && playerState != PlayerState.Unready && !ReadyPlayers.Instance.readyPlayers.Contains(this))
		{
            _audioHandler.PlaySound(26);

            //Add the Player to ready players list.
            ReadyPlayers.Instance.readyPlayers.Add(this);
		}
		else if(playerState != PlayerState.Ready && playerState == PlayerState.Unready && ReadyPlayers.Instance.readyPlayers.Contains(this))
		{
			ReadyPlayers.Instance.readyPlayers.Remove(this);
		}
	}

	public void CheckInCharacterSelect()
	{
		if(panelState == PanelState.CharacterSelect && !ReadyPlayers.Instance.playersInCharacterSelect.Contains(this))
		{
			//Add this Hub to PlayersInCharacterSelect List.
			ReadyPlayers.Instance.playersInCharacterSelect.Add(this);
		}
		else if(panelState != PanelState.CharacterSelect && ReadyPlayers.Instance.playersInCharacterSelect.Contains(this))
		{
			//Remove this Hub to PlayersInCharacterSelect List.
			ReadyPlayers.Instance.playersInCharacterSelect.Remove(this);
		}
	}

}