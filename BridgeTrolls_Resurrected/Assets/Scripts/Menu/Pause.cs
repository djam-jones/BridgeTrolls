using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    [SerializeField]private Canvas pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && pauseMenu.enabled == false)
        {
            Debug.Log("disable");
            pauseMenu.enabled = true;
            Input.ResetInputAxes();
            GetComponent<DeathWall>().enabled = false;

        }
        else if (Input.GetKeyDown(KeyCode.A) && pauseMenu.enabled == true)
        {
            pauseMenu.enabled = false;
        }
    }
}