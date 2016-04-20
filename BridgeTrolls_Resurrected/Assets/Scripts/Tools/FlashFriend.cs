using UnityEngine;
using System.Collections;

public class FlashFriend : MonoBehaviour
{
    #region Vars
    private bool flashing;

    private int flashAmt;
    private int desiredFlashAmt;

    private SpriteRenderer tRenderer;
    private Color desColor;

    // Timer vars
    private float currentTime;
    private float targetTime;

    #endregion

    #region Methods

    private void Start()
    {
        targetTime = 0.7f;
        desiredFlashAmt = 3;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            flash(this.GetComponent<SpriteRenderer>(), Color.red);
        }
        
        // Check if activated.
        if(flashing == true)
        {
            // If activated start timing;
            currentTime += Time.deltaTime;

            Debug.Log(flashAmt + " " + desiredFlashAmt);

            // Check if timing exceded desired time.
            if (currentTime > targetTime)
            {
                // Has it flashed the desirved amount of flashes?
                if (flashAmt > desiredFlashAmt)
                {
                    tRenderer.color = Color.white;
                    flashing = false;
                    flashAmt = 0;
                    return;
                }
                

                // Check current color, then set to opposite.
                if (tRenderer.color == Color.white)
                {
                    tRenderer.color = desColor;
                }
                else
                {
                    tRenderer.color = Color.white;
                }

                // Resetting vars.
                
                currentTime = 0f;
                flashAmt += 1;
            }
        }
    }

    public void flash(SpriteRenderer targetsRenderer, Color desiredColor)
    {
        tRenderer = targetsRenderer;
        desColor = desiredColor;
        targetsRenderer.color = desiredColor;
        flashing = true;
    }

    #endregion

}
