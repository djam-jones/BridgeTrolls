using UnityEngine;
using System.Collections;

public class CharacterDatabase : MonoBehaviour {

	public const string CHARACTER01 = "Character01";
	public const string CHARACTER02 = "Character02";
	public const string CHARACTER03 = "Character03";
	public const string CHARACTER04 = "Character04";

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
			color = new Color(255, 120, 0); //Orange
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
}