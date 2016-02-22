using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    public int score;
	// Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Finish"))
        {
            score += 1;
        }
    }
}
