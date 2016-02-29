using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private bool RightSpot;
    void OnTriggerEnter2D(Collider2D other)
    {
        ScoreC player = other.GetComponent<ScoreC>();
        if(RightSpot == true && player.beenRight == false)
        {
            player.beenRight = true;
            player.score += 1;
            Debug.Log(player.score);
        }
        else if(RightSpot == false && player.beenRight == true)
        {
            player.beenRight = false;
            player.score += 1;
            Debug.Log(player.score);
        }
    }
}
