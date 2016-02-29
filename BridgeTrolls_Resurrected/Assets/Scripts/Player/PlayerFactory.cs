using UnityEngine;
using System.Collections;

public class PlayerFactory : MonoBehaviour {

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

		string _animatorName = "";

		//Sprite Implementation
		Texture2D playerSprite = Resources.Load("Triangle") as Texture2D;
		_spriteRenderer = _playerObj.AddComponent<SpriteRenderer>();
		_spriteRenderer.sprite = Sprite.Create(playerSprite, new Rect(0, 0, playerSprite.width, playerSprite.height), new Vector2(0.5f, 0.5f));
		_spriteRenderer.color = Color.green;


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
		_player.playerNum = playerID;

		//Script Implementations
		_movement = _playerObj.AddComponent<Movement>();
		_roles = _playerObj.AddComponent<PlayerRoles>();
		_score = _playerObj.AddComponent<Score>();
		_switch = _playerObj.AddComponent<SwitchTeam>();

		switch(playerConstString)
		{
			
		}

		//Implement Player Arrow/Color Indication?

		_anim.runtimeAnimatorController = Resources.Load("Animations/Characters" + _animatorName) as RuntimeAnimatorController;

		return _playerObj;
	}
}
