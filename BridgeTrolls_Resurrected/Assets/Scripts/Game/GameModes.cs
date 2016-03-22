using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Modes
{
	Standard, 
	KingOfTheBridge, 
	ArrowEscape, 
	Stocks
}

public class GameModes : MonoBehaviour {

	//Instance
	public static GameModes Instance {get; private set;}

	//Enumerator
	[HideInInspector] 
	public Modes gameMode;
	[HideInInspector]
	public int gameModeIndex;

	//Savable Game Mode Points;
	[SerializeField]
	public int amountOfGamePoints;

	//Standard Game Mode
	public int initialTotalGamePoints = 5;
	public int minTotalGamePoints = 3;
	public int maxTotalGamePoints = 99;

	//Time Game Mode
	public int gameTime = 5;
	public int minGameTime = 1;
	public int maxGameTime = 99;

	//Stocks Game Mode
	public int initialStocks = 3;
	public int minimumStocks = 1;
	public int maximumStocks = 9;

	//UI Elements
	public Text pointsText;
	public Button leftSelectionArrow;
	public Button rightSelectionArrow;

	void Awake()
	{
		gameModeIndex = 0;
	}

	void FixedUpdate()
	{
		SwitchGameModes(gameModeIndex);
		if(pointsText != null)
			pointsText.text = amountOfGamePoints.ToString();
		else{}
	}

	private void SwitchGameModes(int index)
	{
		switch(index)
		{
		case 0:
			gameMode = Modes.Standard;
			amountOfGamePoints = initialTotalGamePoints;
			break;
		case 1:
			gameMode = Modes.KingOfTheBridge;
			amountOfGamePoints = gameTime;
			break;
		case 2:
			gameMode = Modes.ArrowEscape;
			//amountOfGamePoints = initialStocks;
			break;
		case 3:
			gameMode = Modes.Stocks;
			amountOfGamePoints = initialStocks;
			break;
		}
	}

	public void IncreasePoint()
	{
		if(gameMode == Modes.Standard)
		{
			if(initialTotalGamePoints < maxTotalGamePoints)
			{
				initialTotalGamePoints++;
				//rightSelectionArrow.gameObject.SetActive(true);
			}
			else
			{
				initialTotalGamePoints = maxTotalGamePoints;
				//rightSelectionArrow.gameObject.SetActive(false);
			}
		}
	}

	public void DecreasePoint()
	{
		if(gameMode == Modes.Standard)
		{
			if(initialTotalGamePoints > minTotalGamePoints)
			{
				initialTotalGamePoints--;
				//leftSelectionArrow.gameObject.SetActive(true);
			}
			else
			{
				initialTotalGamePoints = minTotalGamePoints;
				//leftSelectionArrow.gameObject.SetActive(false);
			}
		}
	}

}