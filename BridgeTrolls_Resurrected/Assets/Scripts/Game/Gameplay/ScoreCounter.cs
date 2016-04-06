using UnityEngine;
using System.Collections;
using System;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private bool RightSpot;
    private Action<GameObject> addPlayer;

    void Start()
    {
        addPlayer = this.GetComponent<SafeZone>().AddDisabledPlayer;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            handlePlayer(other);
        }
    }

    private void handlePlayer(Collider2D other)
    {
        ScoreC player = other.GetComponent<ScoreC>();
        if (RightSpot == true && player.beenRight == false)
        {
            player.beenRight = true;
            player.score += 1;
            addPlayer(other.gameObject);
        }
        else if (RightSpot == false && player.beenRight == true)
        {
            player.beenRight = false;
            player.score += 1;
            addPlayer(other.gameObject);
        }
    }
}
