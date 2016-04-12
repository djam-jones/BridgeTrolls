using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour
{
    [SerializeField]
    private Vector2 hingeOffset = new Vector2(0, -1);
    [SerializeField]
    private string grabButton = "Fire1_P";
    private GameObject player;
    private bool grabbing = false;

    // Timer vars
    private float currentTime;
    private float targetTime = 1;

    private float cdTimer;
    [SerializeField]
    private float coolDown = 3;

    private float releasetimer;
    [SerializeField]
    private float releaseTime = 15;


    private void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag == "Player" && other.GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && other.GetComponent<HingeJoint2D>() == null && Input.GetButtonDown(grabButton + GetComponent<Player>().playerNum) && cdTimer >= coolDown)
        {
            player = other.gameObject;
            connectPlayers();
        }
    }

    private void connectPlayers()
    {
        
		player.GetComponent<Rigidbody2D>().freezeRotation = true;
        HingeJoint2D connector = player.gameObject.AddComponent<HingeJoint2D>();
        connector.anchor = hingeOffset;
        connector.connectedBody = this.gameObject.GetComponent<Rigidbody2D>();
        player.GetComponent<Movement>().enabled = false;
		player.transform.GetChild(0).gameObject.SetActive(true);
		player.transform.GetChild(0).GetComponent<TapFree>().releaseFunc = disconnectPlayers;
        grabbing = true;
    }

    private void disconnectPlayers()
    {
        grabbing = false;  
        player.GetComponent<Rigidbody2D>().freezeRotation = true;
        player.transform.rotation = Quaternion.identity;    
        Destroy(player.GetComponent<HingeJoint2D>());
        player.GetComponent<Movement>().enabled = true;
		player.transform.GetChild(0).gameObject.SetActive(false);
        player = null;
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;
        if(grabbing == true && currentTime < targetTime)
        {
            currentTime += Time.deltaTime;
        }

		if(grabbing == true && Input.GetButtonDown(grabButton + GetComponent<Player>().playerNum) && currentTime >= targetTime)
        {
            disconnectPlayers();
            currentTime = 0;
            cdTimer = 0;
            releasetimer = 0;
        }
        
        if(grabbing == true)
        {
            releasetimer += Time.deltaTime;
        }
        if(releasetimer >= releaseTime && grabbing == true)
        {
            disconnectPlayers();
            currentTime = 0;
            cdTimer = 0;
            releasetimer = 0;
        }

		if(cdTimer < coolDown)
			GetComponent<AbilityHandler>().Cooldown( transform.GetChild(2).GetComponent<SpriteRenderer>() );
		else
			transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
    }   
}