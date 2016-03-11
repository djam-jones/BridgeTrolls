using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {
    [SerializeField]private GameObject winscreen;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (gameOver == true)
        //{
            if (winscreen.transform.position.y > 500)
            {
                Debug.Log(winscreen.transform.position.y);
                winscreen.transform.position += new Vector3(0, -5, 0);
            }
        //}
	}
}
