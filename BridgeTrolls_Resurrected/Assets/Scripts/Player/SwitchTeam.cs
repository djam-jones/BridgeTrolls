using UnityEngine;
using System.Collections;

public class SwitchTeam : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))
        {
            Debug.Log("enemy");
            tag = "Enemy"; 
            transform.position = new Vector2(0, 0);
            //Destroy(GetComponent<Movement>());
            PlayerRoles roles = GetComponent<PlayerRoles>();
            roles.ChangeRole(); 
            Destroy(GetComponent<Score>());
            Destroy(GetComponent<SwitchTeam>());
        }
    }
}
