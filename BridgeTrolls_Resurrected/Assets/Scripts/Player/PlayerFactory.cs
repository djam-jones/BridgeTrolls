using UnityEngine;
using System.Collections;

public class PlayerFactory : MonoBehaviour {

	/// <summary>
	/// Creates the player.
	/// </summary>
	/// <returns>The player.</returns>
	/// <param name="playerConstString">Player constant string.</param>
	/// <param name="playerID">Player ID.</param>
	public static GameObject CreatePlayer(string playerConstString, int playerID)
	{
		GameObject _playerObj = new GameObject();
		GameObject _childObj = new GameObject();
		GameObject _barObj = new GameObject();
		GameObject _grabStuff = new GameObject();
		GameObject _tapIndicator = new GameObject();
		Rigidbody2D _rigid2D;
		Animator _anim;
		SpriteRenderer _spriteRenderer;
		BoxCollider2D _boxCollider;
		BoxCollider2D _boxTrigger;

		Player _player;
		Movement _movement;
		PlayerRoles _roles;
		ScoreC _score;
		SwitchTeam _switch;
		AbilityHandler _abilityHandler;
		TapFree _tapFree;
		SpriteStraightener _straightener;

		string _animatorName = "";
		Vector2 _boxColliderOffset;
		Vector2 _boxColliderSize;
		Vector2 _boxTriggerOffset;
		Vector2 _boxTriggerSize;
		Animator _smokeAnim;
		Animator _tapAnim;


		//Sprite Implementation
		Texture2D playerSprite = Resources.Load("Sprites/Players/GoblinIdle") as Texture2D;
		_spriteRenderer = _playerObj.AddComponent<SpriteRenderer>();
		_spriteRenderer.sprite = Sprite.Create(playerSprite, new Rect(0, 0, playerSprite.width, playerSprite.height), new Vector2(0.5f, 0.5f));

		//Rigidbody2D Implementation
		_rigid2D = _playerObj.AddComponent<Rigidbody2D>();
		_rigid2D.gravityScale = 0f;
		_rigid2D.freezeRotation = true;

		//Animator Implementation
		_anim = _playerObj.AddComponent<Animator>();

		//Collision Implementation
		_boxCollider = _playerObj.AddComponent<BoxCollider2D>();

		_boxColliderOffset = _boxCollider.offset;
		_boxColliderSize = _boxCollider.size;

		_boxColliderOffset.x = -0.025f;
		_boxColliderOffset.y = -0.375f;
		_boxColliderSize.x = 0.5f;
		_boxColliderSize.y = 0.35f;

		_boxCollider.offset = _boxColliderOffset;
		_boxCollider.size = _boxColliderSize;

		//Trigger Implementation
		_boxTrigger = _playerObj.AddComponent<BoxCollider2D>();
		_boxTrigger.isTrigger = true;

		/*_boxTriggerOffset = _boxTrigger.offset;
		_boxTriggerSize = _boxTrigger.size;

		_boxTriggerOffset.x = -0.025f;
		_boxTriggerOffset.y = -0.375f;
		_boxTriggerSize.x  = 0.5f;
		_boxTriggerSize.y = 0.35f;

		_boxTrigger.offset = _boxTriggerOffset;
		_boxTrigger.size = _boxTriggerSize;*/

		//Player Script Implementation
		_player = _playerObj.AddComponent<Player>();
		_player.playerType = playerConstString;
		_player.playerNum = playerID;

		//Script Implementations
		_roles = _playerObj.AddComponent<PlayerRoles>();

		_movement = _playerObj.AddComponent<Movement>();
		_movement.speed = 4f;

		_score = _playerObj.AddComponent<ScoreC>();
		_switch = _playerObj.AddComponent<SwitchTeam>();
		_abilityHandler = _playerObj.AddComponent<AbilityHandler>();

		//Grab Stuff Implementation
		_grabStuff.name = "Grab Stuff";
		_grabStuff.transform.parent = _playerObj.transform;
		_grabStuff.transform.position = new Vector2(0, 0f);
		_grabStuff.SetActive(false);
		_tapFree = _grabStuff.AddComponent<TapFree>();

		//Bar GameObject Implementation
		_barObj.name = "Tapping Bar";
		_barObj.transform.parent = _grabStuff.transform;
		_barObj.transform.position = new Vector2(0, 0.75f);
		_barObj.transform.localScale = new Vector3(1, 0.1f, 1);

		Texture2D barSprite = Resources.Load("Square-2") as Texture2D;
		_spriteRenderer = _barObj.AddComponent<SpriteRenderer>();
		_spriteRenderer.sprite = Sprite.Create(barSprite, new Rect(0, 0, barSprite.width, barSprite.height), new Vector2(0.5f, 0.5f));
		_spriteRenderer.color = new Color(1f, 0.1f, 0.1f);
		_spriteRenderer.GetComponent<SpriteRenderer>().sortingOrder = 11;

		//Tap Indicator GameObject Implementation
		_tapIndicator.name = "Tap Button";
		_tapIndicator.transform.parent = _grabStuff.transform;
		_tapIndicator.transform.position = new Vector2(0, 0.925f);

		_spriteRenderer = _tapIndicator.AddComponent<SpriteRenderer>();
		_spriteRenderer.GetComponent<SpriteRenderer>().sortingOrder = 12;
		_tapAnim = _tapIndicator.AddComponent<Animator>();
		_tapAnim.runtimeAnimatorController = Resources.Load("Animations/Tap/Tap_Indicator_Animator") as RuntimeAnimatorController;

		//Child GameObject Implementation
//		_childObj.name = "Straighthened Sprite";
//		_childObj.transform.parent = _playerObj.transform;
//		_straightener = _childObj.AddComponent<SpriteStraightener>();
//
//		Texture2D childSprite = Resources.Load("Sprites/Players/GoblinIdle") as Texture2D;
//		_spriteRenderer = _childObj.AddComponent<SpriteRenderer>();
//		_spriteRenderer.sprite = Sprite.Create(playerSprite, new Rect(0, 0, childSprite.width, childSprite.height), new Vector2(0.5f, 0.5f));


		//Smoke GameObject Implementation
		_childObj.name = "Smoke Poof";
		_childObj.transform.parent = _playerObj.transform;

		_spriteRenderer = _childObj.AddComponent<SpriteRenderer>();
		_straightener = _childObj.AddComponent<SpriteStraightener>();
		_smokeAnim = _childObj.AddComponent<Animator>();
		_smokeAnim.runtimeAnimatorController = Resources.Load("Animations/Player Effects/Smoke_Animator") as RuntimeAnimatorController;

		switch(playerConstString)
		{
			case CharacterDatabase.CHARACTER01:
				_animatorName = "Character_One_Animator";
				break;
			case CharacterDatabase.CHARACTER02:
				_animatorName = "Character_Two_Animator";
				break;
			case CharacterDatabase.CHARACTER03:
				_animatorName = "Character_Three_Animator";
				break;
			case CharacterDatabase.CHARACTER04:
				_animatorName = "Character_Four_Animator";
				break;
			case CharacterDatabase.TROLL:
				_animatorName = "Troll_Animator";
				break;
			case CharacterDatabase.MINION:
				_animatorName = "Minion_Animator";
				break;
		}

		//Implement Player Arrow/Color Indication
		PlayerIndicator indicator = _playerObj.AddComponent<PlayerIndicator>();
		indicator.Init();
//		indicator.SetColor(CharacterDatabase.GetColorById(playerID));
		indicator.SetSprite(CharacterDatabase.GetSpriteById(playerID));

		_anim.runtimeAnimatorController = Resources.Load("Animations/Characters/" + _animatorName) as RuntimeAnimatorController;

		_player.SetCharacter(playerConstString);
		return _playerObj;
	}
}