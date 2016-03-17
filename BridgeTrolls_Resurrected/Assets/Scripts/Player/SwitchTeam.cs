using UnityEngine;
using System.Collections;

public class SwitchTeam : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))
        {
            tag = "Minion"; 
            transform.position = new Vector2(0, 0);
            PlayerRoles roles = GetComponent<PlayerRoles>();
            roles.ChangeRole();
			GetComponent<SpriteRenderer>().color = new Color(0.56f, 0, 0.8f);

			if(!this.gameObject.GetComponent<Grab>())	
				this.gameObject.AddComponent<Grab>();
            
			Destroy(GetComponent<Score>());
            Destroy(GetComponent<SwitchTeam>());
        }
    }
}
