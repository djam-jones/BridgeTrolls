using UnityEngine;
using System.Collections;

public class SwitchTeam : MonoBehaviour 
{
	[SerializeField] private RuntimeAnimatorController _minionAnimator;

	PlayerRoles roles;
	Player player;

	void Awake()
	{
		_minionAnimator = Resources.Load("Animations/Characters/Minion_Animator") as RuntimeAnimatorController;
		roles = GetComponent<PlayerRoles>();
		player = GetComponent<Player>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (/*other.tag == Tags.TROLL_TAG || */other.tag == Tags.ARROW_TAG)
        {
			Switch();
        }
    }

	public void Switch()
	{
		if(roles.playerRoles != Roles.Hostile)
		{
			tag = Tags.MINION_TAG; 
			StartCoroutine(player.Poof());
			roles.ChangeRole();
			//GetComponent<SpriteRenderer>().sprite = Sprite.Create(_minionSprite, new Rect(0, 0, _minionSprite.width, _minionSprite.height), new Vector2(0.5f, 0.5f));
			GetComponent<Animator>().runtimeAnimatorController = _minionAnimator;
			
			if(!this.gameObject.GetComponent<Grab>())	
				this.gameObject.AddComponent<Grab>();
			
			Destroy(GetComponent<TapFree>());
			Destroy(GetComponent<Score>());
			Destroy(GetComponent<SwitchTeam>());
		}
	}
}
