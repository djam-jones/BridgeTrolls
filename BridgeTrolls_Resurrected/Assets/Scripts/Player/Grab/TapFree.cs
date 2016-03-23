using UnityEngine;
using System.Collections;
using System;

public class TapFree : MonoBehaviour
{
    private GameObject bar;
    [SerializeField]
    private float strength = 0.75f;
    [SerializeField]
    private float growAmt = 0.15f;
    [SerializeField]
    private float shrinkAmt = 0.25f;
    [SerializeField]
    private float barSize = 0.9f;
	[SerializeField, HideInInspector]
    private string tapButton = "Fire1_P";

    public Action releaseFunc;

    private void Start()
    {
        bar = this.transform.GetChild(0).gameObject;
        
        if(bar == null)
        {
            Debug.Log("No sprite found");
            
        }
        else
        {
            bar.transform.localScale = new Vector3(0, 0.1f, 0.1f);
        }
       // Debug.Log(releaseFunc);   
    }

    private void Update()
    {
        //Debug.Log(grabber);
		if(Input.GetButtonDown(tapButton + GetComponentInParent<Player>().playerNum.ToString()))
        {
            bar.transform.localScale += new Vector3(growAmt, 0, 0);
        }

        if(bar.transform.localScale.x > 0.1)
        {
            bar.transform.localScale -= new Vector3(1 * Time.deltaTime * strength, 0, 0);
        }
        else
        {
            bar.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        
        if (bar.transform.localScale.x > barSize)
        {
            if(releaseFunc != null)
            {
                bar.transform.localScale = new Vector3(0, 0.1f, 0.1f);
                releaseFunc();
                releaseFunc = null;
            }
        }
    }
}