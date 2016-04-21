using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

	[SerializeField, HideInInspector] private string _rightControls = "RightTrigger";
	[SerializeField, HideInInspector] private string _leftControls = "LeftTrigger";

	public string[] playableCharacters = new string[2];
	private string _selectedCharacterName;

	public Sprite[] characterSprites = new Sprite[2];
	private Sprite _selectedCharacterSprite;

	private int _characterIndex;
	public Image characterHolder;

	void Update()
	{
		_selectedCharacterName = playableCharacters[_characterIndex];
		_selectedCharacterSprite = characterSprites[_characterIndex];

		characterHolder.sprite = _selectedCharacterSprite;
	}

	public void Select()
	{
//		if(Input.GetButtonDown(_rightControls + GetComponent<HubHandler>().panelID))
		if(Input.GetButtonDown(_rightControls + ControllerAssigner.Instance.allControllersInOrder[ControllerAssigner.Instance.ControllerIndex]))
		{
			NextCharacter();
			NextSprite();

			if(_characterIndex <= playableCharacters.Length -2)
			{
				_characterIndex++;
			}
		}
//		else if(Input.GetButtonDown(_leftControls + GetComponent<HubHandler>().panelID))
		else if(Input.GetButtonDown(_leftControls + ControllerAssigner.Instance.allControllersInOrder[ControllerAssigner.Instance.ControllerIndex]))
		{
			PreviousCharacter();
			PreviousSprite();

			if(_characterIndex > 0)
			{
				_characterIndex--;
			}
		}
	}

	public void Confirm()
	{
		for(int i = 0; i < 8; i++)
		{
			_selectedCharacterName = playableCharacters[_characterIndex];
			_selectedCharacterSprite = characterSprites[_characterIndex];

            CharacterDatabase.GetCharacterByInt(i);
			PlayerPrefs.SetString("CharacterName" + i.ToString(), _selectedCharacterName);
            
			print(_selectedCharacterName + " Confirmed!");
		}
    }

	/// <summary>
	/// Goes to the Next String in the array for the selected character.
	/// </summary>
	/// <returns>The character.</returns>
	private string NextCharacter()
	{
		for(int i = 0; i < playableCharacters.Length; i++)
		{
			if(playableCharacters[i] == _selectedCharacterName && i < playableCharacters.Length -1)
			{
				_selectedCharacterName = playableCharacters[i++];
				break;
			}
			else
				_selectedCharacterName = playableCharacters[0];
		}
		return _selectedCharacterName;
	}

	/// <summary>
	/// Goes to the Previous String in the array for the selected character.
	/// </summary>
	/// <returns>The character.</returns>
	private string PreviousCharacter()
	{
		for(int i = 0; i < playableCharacters.Length; i++)
		{
			if(playableCharacters[i] == _selectedCharacterName && i < playableCharacters.Length -1)
			{
				_selectedCharacterName = playableCharacters[i--];
				break;
			}
			else
				_selectedCharacterName = playableCharacters[0];
		}
		return _selectedCharacterName;
	}

	/// <summary>
	/// Goes to the Next Sprite in the array for the selected character.
	/// </summary>
	/// <returns>The sprite.</returns>
	private Sprite NextSprite()
	{
		for(int i = 0; i < characterSprites.Length; i++)
		{
			if(characterSprites[i] == _selectedCharacterSprite && i < characterSprites.Length -1)
			{
				_selectedCharacterSprite = characterSprites[i++];
				break;
			}
			else
				_selectedCharacterSprite = characterSprites[0];
		}
		return _selectedCharacterSprite;
	}

	/// <summary>
	/// Goes to the Previous Sprite in the array for the selected character.
	/// </summary>
	/// <returns>The sprite.</returns>
	private Sprite PreviousSprite()
	{
		for(int i = 0; i < characterSprites.Length; i++)
		{
			if(characterSprites[i] == _selectedCharacterSprite && i < characterSprites.Length -1)
			{
				_selectedCharacterSprite = characterSprites[i--];
				break;
			}
			else
				_selectedCharacterSprite = characterSprites[0];
		}
		return _selectedCharacterSprite;
	}
}