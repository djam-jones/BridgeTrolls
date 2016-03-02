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
		Rigidbody2D _rigid2D;
		Animator _anim;
		SpriteRenderer _spriteRenderer;
		BoxCollider2D _boxCollider;
		BoxCollider2D _boxTrigger;

		Player _player;
		Movement _movement;
		PlayerRoles _roles;
		Score _score;
		SwitchTeam _switch;
		AbilityHandler _abilityHandler;

		string _animatorName = "";

		//Sprite Implementation
		Texture2D playerSprite = Resources.Load("Sprites/GoblinRun") as Texture2D;
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
		_movement.speed = 3f;

		_score = _playerObj.AddComponent<Score>();
		_switch = _playerObj.AddComponent<SwitchTeam>();
		_abilityHandler = _playerObj.AddComponent<AbilityHandler>();

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
		}

		//Implement Player Arrow/Color Indication?
		PlayerIndicator indicator = _playerObj.AddComponent<PlayerIndicator>();
		indicator.Init();
		indicator.SetColor(CharacterDatabase.GetColorById(playerID));

		_anim.runtimeAnimatorController = Resources.Load("Animations/Characters/" + _animatorName) as RuntimeAnimatorController;

		_player.SetCharacter(playerConstString);
		return _playerObj;
	}
}