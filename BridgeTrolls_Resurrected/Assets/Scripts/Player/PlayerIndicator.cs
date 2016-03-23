using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {

	private GameObject _indicator;
	[HideInInspector] public float yValue = 0.85f;

	public void Init()
	{
		GameObject newIndicator = Resources.Load("PlayerArrow", typeof(GameObject)) as GameObject;
		_indicator = Instantiate(newIndicator, this.transform.position + new Vector3(0f, yValue, 0f), newIndicator.transform.rotation) as GameObject;
		_indicator.transform.parent = this.transform;
		_indicator.GetComponent<SpriteRenderer>().sortingOrder = 10;
	}

	public void SetColor(Color color)
	{
		SpriteRenderer spriteRenderer = _indicator.GetComponent<SpriteRenderer>();
		color.a = 0.75f;
		spriteRenderer.color = color;
	}

	public void SetSprite(Texture2D sprite)
	{
		SpriteRenderer spriteRenderer = _indicator.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), new Vector2(0.5f, 0.5f));
	}
}