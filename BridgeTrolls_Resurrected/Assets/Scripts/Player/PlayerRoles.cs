using UnityEngine;
using System.Collections;

public enum Roles
{
	Hostile, 
	Neutral
}

public class PlayerRoles : MonoBehaviour {

	public Roles playerRoles;

	public static PlayerRoles Instance {get; private set;}

	void Awake()
	{
		Instance = this;
	}

	public void ChangeRole()
	{
		if(playerRoles == Roles.Hostile)
			playerRoles = Roles.Neutral;
		else if(playerRoles == Roles.Neutral)
			playerRoles = Roles.Hostile;
	}

}
