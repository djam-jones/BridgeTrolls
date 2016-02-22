using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
    public int score = -1;
    public bool infinish;
    [SerializeField]private GameMagager magager;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Finish"))
        {
            score += 1;
            infinish = true;

            for (int i = 0; i < magager.playerArray.Count; i++)
            {
                Debug.Log(i);
            } 

            if (infinish == true)
            {
                GetComponent<Movement>().enabled = false;
                transform.position = new Vector2(8.4f, transform.position.y);
            }
            else if (infinish == false)
            {
                GetComponent<Movement>().enabled = true;
            }

        }
    }
}
