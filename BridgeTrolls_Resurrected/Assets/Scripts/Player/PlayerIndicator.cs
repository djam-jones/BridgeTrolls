using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {

	private GameObject _indicator;

	public void Init()
	{
		GameObject newIndicator = Resources.Load("PlayerArrow", typeof(GameObject)) as GameObject;
		_indicator = Instantiate(newIndicator, this.transform.position + new Vector3(0, 1f, 0), newIndicator.transform.rotation) as GameObject;
		_indicator.transform.parent = this.transform;
		_indicator.transform.localScale *= 0.32f;
	}

	public void SetColor(Color color)
	{
		SpriteRenderer spriteRenderer = _indicator.GetComponent<SpriteRenderer>();
		color.a = 0.5f;
		spriteRenderer.color = color;
	}
}