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

		_boxColliderOffset.y = -0.5f;
		_boxColliderSize.x = 0.35f;
		_boxColliderSize.y = 0.2f;

		_boxCollider.offset = _boxColliderOffset;
		_boxCollider.size = _boxColliderSize;

		//Trigger Implementation
		_boxTrigger = _playerObj.AddComponent<BoxCollider2D>();
		_boxTrigger.isTrigger = true;


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
		_tapFree = _playerObj.AddComponent<TapFree>();

		//Bar GameObject Implementation
		_barObj.transform.parent = _playerObj.transform;
		_barObj.transform.position = new Vector2(0, 0.75f);

		Texture2D barSprite = Resources.Load("Square-2") as Texture2D;
		_spriteRenderer = _barObj.AddComponent<SpriteRenderer>();
		_spriteRenderer.sprite = Sprite.Create(barSprite, new Rect(0, 0, barSprite.width, barSprite.height / 2), new Vector2(0.5f, 0.5f));
		_spriteRenderer.color = new Color(0f, 0.5f, 0.16f);

		//Child GameObject Implementation
		_childObj.transform.parent = _playerObj.transform;
		_straightener = _childObj.AddComponent<SpriteStraightener>();

		Texture2D childSprite = Resources.Load("Sprites/Players/GoblinIdle") as Texture2D;
		_spriteRenderer = _childObj.AddComponent<SpriteRenderer>();
		_spriteRenderer.sprite = Sprite.Create(playerSprite, new Rect(0, 0, childSprite.width, childSprite.height), new Vector2(0.5f, 0.5f));

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
		}

		//Implement Player Arrow/Color Indication
		PlayerIndicator indicator = _playerObj.AddComponent<PlayerIndicator>();
		indicator.Init();
		indicator.SetColor(CharacterDatabase.GetColorById(playerID));
//		indicator.SetSprite(CharacterDatabase.GetSpriteById(playerID));

		_anim.runtimeAnimatorController = Resources.Load("Animations/Characters/" + _animatorName) as RuntimeAnimatorController;

		_player.SetCharacter(playerConstString);
		return _playerObj;
	}
}