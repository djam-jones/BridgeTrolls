using UnityEngine;
using System.Collections;

public class ScoreC : MonoBehaviour
{
    public int score;
    public bool beenRight = false;

	private GameModes _gameModesScript;

	void Awake()
	{
		_gameModesScript = GameObject.Find("GameHandeler").GetComponent<GameModes>();
	}

	void Update()
	{
		StandardGameModeInstance();
	}

	private void StandardGameModeInstance()
	{
		if(_gameModesScript.gameMode == Modes.Standard)
		{
			if(score == PlayerPrefs.GetInt("AmountOfGamePoints") && GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && _gameModesScript.gameObject.GetComponent<GameMagager>().allGoblins.Count != 0)
			{
				//Win Game.
				print ("Player " + (gameObject.GetComponent<Player>().playerNum + 1) + " Won!");
				PlayerPrefs.SetString( "PlayerThatWon", "Player " + (gameObject.GetComponent<Player>().playerNum + 1).ToString() );

				_gameModesScript.gameObject.GetComponent<GameMagager>().gameOver = true;
			}
			else if(GetComponent<PlayerRoles>().playerRoles == Roles.Hostile && _gameModesScript.gameObject.GetComponent<GameMagager>().allGoblins.Count == 0)
			{
				print ("Troll Player " + (gameObject.GetComponent<Player>().playerNum + 1) + " Won!");
				PlayerPrefs.SetString( "PlayerThatWon", "Troll Player " + (gameObject.GetComponent<Player>().playerNum + 1).ToString() );

				_gameModesScript.gameObject.GetComponent<GameMagager>().gameOver = true;
			}
		}
	}
}
