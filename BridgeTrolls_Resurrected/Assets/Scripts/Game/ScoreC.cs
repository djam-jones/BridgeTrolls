using UnityEngine;
using System.Collections;

public class ScoreC : MonoBehaviour
{
    public int score;
    public bool beenRight = false;

	private GameObject _gameManager;
	private GameModes _gameModesScript;
	private GameMagager _gameManagerScript;

	void Awake()
	{
		_gameManager = GameObject.Find("GameHandeler");
		_gameModesScript = _gameManager.GetComponent<GameModes>();
		_gameManagerScript = _gameManager.GetComponent<GameMagager>();
	}

	void Update()
	{
		CheckRightList();
		StandardGameModeInstance();
	}

	public void CheckRightList()
	{
		if( beenRight && !_gameManagerScript.rightSidedPlayers.Contains(this.gameObject) )
		{
			_gameManagerScript.rightSidedPlayers.Add(this.gameObject);
		}
		else if(!beenRight && _gameManagerScript.rightSidedPlayers.Contains(this.gameObject))
		{
			_gameManagerScript.rightSidedPlayers.Remove(this.gameObject);
		}
	}

	private void StandardGameModeInstance()
	{
		if(_gameModesScript.gameMode == Modes.Standard)
		{
			if(score == PlayerPrefs.GetInt("AmountOfGamePoints") && GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && _gameManagerScript.allGoblins.Count != 0)
			{
				//Win Game.
				print ("Player " + (gameObject.GetComponent<Player>().playerNum + 1) + " Won!");
				PlayerPrefs.SetString( "PlayerThatWon", "Player " + (gameObject.GetComponent<Player>().playerNum + 1).ToString() );

				_gameManagerScript.gameOver = true;
			}
			else if(GetComponent<PlayerRoles>().playerRoles == Roles.Hostile && _gameManagerScript.allGoblins.Count == 0)
			{
				print ("Troll Player " + (gameObject.GetComponent<Player>().playerNum + 1) + " Won!");
				PlayerPrefs.SetString( "PlayerThatWon", "Troll Player " + (gameObject.GetComponent<Player>().playerNum + 1).ToString() );

				_gameManagerScript.gameOver = true;
			}
		}
	}
}
