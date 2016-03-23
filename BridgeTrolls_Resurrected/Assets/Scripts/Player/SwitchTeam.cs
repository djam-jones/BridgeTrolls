using UnityEngine;
using System.Collections;

public class SwitchTeam : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D other)
    {
		PlayerRoles roles = GetComponent<PlayerRoles>();

		if (other.tag == Tags.TROLL_TAG || other.tag == Tags.ARROW_TAG)
        {
			if(roles.playerRoles != Roles.Hostile)
			{
				tag = Tags.MINION_TAG; 
	            transform.position = new Vector2(0, 0);
	            roles.ChangeRole();
				GetComponent<SpriteRenderer>().color = new Color(0.56f, 0, 0.8f);

				if(!this.gameObject.GetComponent<Grab>())	
					this.gameObject.AddComponent<Grab>();
	            
				Destroy(GetComponent<TapFree>());
				Destroy(GetComponent<Score>());
	            Destroy(GetComponent<SwitchTeam>());
			}
        }
    }
}
