using UnityEngine;
using System.Collections;

public class AbilityHandler : MonoBehaviour {

    [SerializeField, HideInInspector] private string _actionKey_A = "Fire1_P";
    [SerializeField, HideInInspector] private string _actionKey_B = "Fire2_P";
    private Roles role;
    private bool DashUp = true;
    private Movement move;
    [SerializeField] private Sprite Dash;
    [SerializeField] private Sprite Idle;

    void Awake()
    {
        role = this.gameObject.GetComponent<PlayerRoles>().playerRoles;
        move = this.gameObject.GetComponent<Movement>();
    }

	void Update()
	{
		UseAbility();
	}

    private void UseAbility()
    {
        if (Input.GetButtonDown(_actionKey_A + GetComponent<Player>().playerNum))
        {
            if (GetComponent<PlayerRoles>().playerRoles == Roles.Hostile)
            {
                Ability_Grab();
            }
            else if (GetComponent<PlayerRoles>().playerRoles == Roles.Neutral && GetComponent<PlayerRoles>().playerRoles != Roles.Hostile && DashUp == true)
            {
                StartCoroutine(Ability_Dash());
            }

            Debug.Log("WOOW A");
        }
        else if (Input.GetButtonDown(_actionKey_B + GetComponent<Player>().playerNum))
        {
            
            Debug.Log("WOOOW B");
        }
    }

    IEnumerator Ability_Dash()
    {
        /*Debug.Log("dash");
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(200,0));
        DashUp = false;
        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(new Vector2(-200, 0));
        yield return new WaitForSeconds(3f);
        DashUp = true;*/

        Debug.Log("dash");
        move.speed += 7;
        GetComponent<SpriteRenderer>().sprite = Dash;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = Idle;
        move.speed -= 7;
        DashUp = false;
        yield return new WaitForSeconds(3);
        DashUp = true;



    }
    public void Ability_Grab()
    {
        Debug.Log("grab");
    }

}
