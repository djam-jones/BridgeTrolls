using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
    public int score = -1;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Finish"))
        {
            score += 1;
        }
    }
}
