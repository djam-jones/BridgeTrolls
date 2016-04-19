using UnityEngine;
using System.Collections;

public class ScratchAbility : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    private bool scratching;
    [SerializeField]
    private GameObject scratchHBox;

    private float currentTime;
    private float targetTime = 0.3f;

    // Use this for initialization
    void Start()
    {
        playerSprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && scratching == false)
        {
            scratching = true;
            scratchHBox.SetActive(true);
            playerSprite.color = new Color(1, 0, 0);
        }
        else if (scratching == true)
        {
            currentTime += Time.deltaTime;
            if(currentTime > targetTime)
            {
                playerSprite.color = Color.white;
                scratchHBox.SetActive(false);
                currentTime = 0;
                scratching = false;
            }
            else
            {

            }
        }
    } 
}
