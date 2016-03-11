using UnityEngine;
using System.Collections;

public class CharacterDatabase : MonoBehaviour {

	public const string CHARACTER01 = "Character01";
	public const string CHARACTER02 = "Character02";
	public const string CHARACTER03 = "Character03";
	public const string CHARACTER04 = "Character04";
	public const string TROLL		= "Troll";

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

	public static Sprite GetSpriteById(int i)
	{
		Sprite sprite = new Sprite();

		switch(i)
		{
		case 0:
			sprite = Resources.Load("Spritesheets/Player_Indicators_0") as Sprite;
			break;
		case 1:
			sprite = Resources.Load("Spritesheets/Player_Indicators_1") as Sprite;
			break;
		case 2:
			sprite = Resources.Load("Spritesheets/Player_Indicators_2") as Sprite;
			break;
		case 3:
			sprite = Resources.Load("Spritesheets/Player_Indicators_3") as Sprite;
			break;
		case 4:
			sprite = Resources.Load("Spritesheets/Player_Indicators_4") as Sprite;
			break;
		case 5:
			sprite = Resources.Load("Spritesheets/Player_Indicators_5") as Sprite;
			break;
		case 6:
			sprite = Resources.Load("Spritesheets/Player_Indicators_6") as Sprite;
			break;
		case 7:
			sprite = Resources.Load("Spritesheets/Player_Indicators_7") as Sprite;
			break;
		}
		return sprite;
	}
}