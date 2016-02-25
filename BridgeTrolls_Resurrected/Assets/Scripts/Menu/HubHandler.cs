using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum PanelState
{
	WaitingToJoin, 
	CharacterSelect, 
	Ready
}

public class HubHandler : MonoBehaviour {

	[SerializeField, HideInInspector] private string _actionKey = "Submit";
	[SerializeField, HideInInspector] private string _cancelKey = "Cancel";

	[HideInInspector] public PanelState _panelState;
	[SerializeField, HideInInspector]private int _panelIndex = 0;

	[SerializeField, HideInInspector] private Text[] _panelTexts = new Text[0];

	void Update()
	{
		CheckState(_panelIndex);
		ProgressState();
		RegressState();
	}

	private void CheckState(int panelIndex)
	{
		switch(panelIndex)
		{
		case 0:
			_panelState = PanelState.WaitingToJoin;
			//Disabled Joystick/D-Pad controls.
			break;
		case 1:
			_panelState = PanelState.CharacterSelect;
			//Enable Joystick/D-Pad controls.
			break;
		case 2:
			_panelState = PanelState.Ready;
			//Disabled Joystick/D-Pad controls.
			break;
		}
	}

	private void ProgressState()
	{
		if(Input.GetButtonDown(_actionKey))
		{
			print("Continue.");

			if(_panelIndex >= 0 && _panelIndex <= 2)
			{
				_panelIndex++;
				print(_panelIndex);
			}
		}
	}

	private void RegressState()
	{
		if(Input.GetButtonDown(_cancelKey))
		{
			print("Back.");

			if(_panelIndex <= 0 && _panelIndex >= 2)
			{
				_panelIndex--;
				print(_panelIndex);
			}
		}
	}

}