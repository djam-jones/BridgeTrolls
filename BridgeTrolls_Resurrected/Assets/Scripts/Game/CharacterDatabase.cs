using UnityEngine;
using System.Collections;

public class CharacterDatabase : MonoBehaviour {

	public const string CHARACTER01 = "Character01";
	public const string CHARACTER02 = "Character02";
	public const string CHARACTER03 = "Character03";
	public const string CHARACTER04 = "Character04";
	public const string TROLL		= "Troll";
	public const string MINION		= "Minion";

	/// <summary>
	/// Gets the character by integer.
	/// </summary>
	/// <returns>The character by int.</returns>
	/// <param name="i">The index.</param>
	public static string GetCharacterByInt(int i)
	{
		switch(i)
		{
			case 0:
				return CHARACTER01;
			case 1:
				return CHARACTER02;
			case 2:
				return CHARACTER03;
			case 3:
				return CHARACTER04;
			case 4:
				return TROLL;
			case 5:
				return MINION;
		}
		return "";
	}

	/// <summary>
	/// Gets the color by Player Identifier.
	/// </summary>
	/// <returns>The color by identifier.</returns>
	/// <param name="i">The index.</param>
	public static Color GetColorById(int i)
	{
		Color color = new Color();

		switch(i)
		{
		case 0:
			color = Color.red;
			break;
		case 1:
			color = Color.blue;
			break;
		case 2:
			color = Color.yellow;
			break;
		case 3:
			color = Color.green;
			break;
		case 4:
			color = new Color(1.0f, 0.47f, 0.0f); //Orange
			break;
		case 5:
			color = Color.cyan;
			break;
		case 6:
			color = Color.magenta;
			break;
		case 7:
			color = Color.grey;
			break;
		}
		return color;
	}

	public static Texture2D GetSpriteById(int i)
	{
		Texture2D sprite = new Texture2D(46, 56);

		switch(i)
		{
		case 0:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_0") as Texture2D;
			break;
		case 1:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_1") as Texture2D;
			break;
		case 2:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_2") as Texture2D;
			break;
		case 3:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_3") as Texture2D;
			break;
		case 4:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_4") as Texture2D;
			break;
		case 5:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_5") as Texture2D;
			break;
		case 6:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_6") as Texture2D;
			break;
		case 7:
			sprite = Resources.Load("Sprites/PlayerIndicators/Player_Indicators_7") as Texture2D;
			break;
		}
		return sprite;
	}
}