﻿using UnityEngine;
using System.Collections;

public class ScoreC : MonoBehaviour
{
    public int score;
    public bool beenRight = false;

	private GameObject _gameHandler;

	void Awake()
	{
		_gameHandler = GameObject.Find("GameHandeler");
	}

	void Update()
	{
		CheckRightList();
		StandardGameModeInstance();
	}

	public void CheckRightList()
	{
		if( beenRight && !_gameHandler.GetComponent<GameMagager>().rightSidedPlayers.Contains(this.gameObject) )
		{
			GameMagager.Instance.rightSidedPlayers.Add(this.gameObject);
		}
		else if(!beenRight && _gameHandler.GetComponent<GameMagager>().rightSidedPlayers.Contains(this.gameObject))
		{
			GameMagager.Instance.rightSidedPlayers.Remove(this.gameObject);
		}
	}

	private void StandardGameModeInstance()
	{
		if(_gameHandler.GetComponent<GameModes>().gameMode == Modes.Standard)
		{
			if(score == PlayerPrefs.GetInt("AmountOfGamePoints") && GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && _gameHandler.GetComponent<GameMagager>().allGoblins.Count != 0)
			{
				//Win Game.
				PlayerPrefs.SetString( "PlayerThatWon", "Player " + (gameObject.GetComponent<Player>().playerNum + 1).ToString() );

				_gameHandler.GetComponent<GameMagager>().gameOver = true;
				_gameHandler.GetComponent<GameMagager>().trollWins = false;
			}
			else if(GetComponent<PlayerRoles>().playerRoles == Roles.Hostile && _gameHandler.GetComponent<GameMagager>().allGoblins.Count == 0)
			{
				_gameHandler.GetComponent<GameMagager>().gameOver = true;
				_gameHandler.GetComponent<GameMagager>().trollWins = true;
			}
		}
	}
}