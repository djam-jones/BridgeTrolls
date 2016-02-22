using UnityEngine;
using System.Collections;

public class SwitchTeam : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))
        {
            tag = "Enemy";
            Destroy(GetComponent<SwitchTeam>());
            Destroy(GetComponent<Score>());
            transform.position = new Vector2(0, 0);
        }
    }
}
