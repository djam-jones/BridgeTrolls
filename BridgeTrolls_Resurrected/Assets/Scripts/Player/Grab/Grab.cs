using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour
{
    [SerializeField]
    private Vector2 hingeOffset = new Vector2(0, -1);
    [SerializeField]
    private string grabButtton = "Fire1";
    private GameObject player;
    private bool grabbing = false;

    // Timer vars
    private float currentTime;
    private float targetTime = 1;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && other.GetComponent<HingeJoint2D>() == null && Input.GetButtonDown(grabButtton))
        {
            player = other.gameObject;
            connectPlayers();
        }
    }

    private void connectPlayers()
    {
        
        player.GetComponent<Rigidbody2D>().freezeRotation = false;
        HingeJoint2D connector = player.gameObject.AddComponent<HingeJoint2D>();
        connector.anchor = hingeOffset;
        connector.connectedBody = this.gameObject.GetComponent<Rigidbody2D>();
        grabbing = true;
    }

    private void disconnectPlayers()
    {
        grabbing = false;  
        player.GetComponent<Rigidbody2D>().freezeRotation = true;
        player.transform.rotation = Quaternion.identity;    
        Destroy(player.GetComponent<HingeJoint2D>());
        player = null;
    }

    private void Update()
    {
        if(grabbing == true && currentTime < targetTime)
        {
            currentTime += Time.deltaTime;
        }

        if(grabbing == true && Input.GetButtonDown(grabButtton) && currentTime >= targetTime)
        {
            disconnectPlayers();
            currentTime = 0;
        }
    }   
}