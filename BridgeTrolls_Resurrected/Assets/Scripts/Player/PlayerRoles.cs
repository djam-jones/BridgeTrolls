using UnityEngine;
using System.Collections;

public enum Roles
{
	Hostile, 
	Neutral, 
	Minion
}

public class PlayerRoles : MonoBehaviour {

	public Roles playerRoles;

	public void ChangeRole()
	{
		if(playerRoles == Roles.Hostile && gameObject.tag != "Enemy")
			playerRoles = Roles.Neutral;
		else if(playerRoles == Roles.Neutral)
			playerRoles = Roles.Minion;
	}

}
