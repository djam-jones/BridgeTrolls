using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeathWall : MonoBehaviour {

	private RuntimeAnimatorController animations;

	[SerializeField]private GameObject arrow;

	[SerializeField]private RuntimeAnimatorController sprite1;
	[SerializeField]private RuntimeAnimatorController sprite2;
	[SerializeField]private RuntimeAnimatorController sprite3;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			StartCoroutine(spawnArrows());
		}
	}

	IEnumerator spawnArrows()
	{
		for (int x = -10; x <= 10; x += 1)
		{

			for (int y = -6; y <=  5; y += 1)
			{
				Vector2 position = new Vector2(x + Random.Range(0.5f,-0.5f),y + Random.Range(0.5f,-0.5f));
				if (position.y > 5.07)
				{
					position.y = 4.9f;
				}
				if(position.x > 10)
				{
					position.x = 10;
				}
				else if(position.x < -10)
				{
					position.x = -10;
				}
				arrow.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)(RandomSprite(animations));
				GameObject newarrow = Instantiate(arrow, position, transform.rotation) as GameObject;
				newarrow.GetComponent<SpriteRenderer>().sortingOrder = ((int)newarrow.transform.position.y *-1) + 7;
				yield return new WaitForSeconds(0.01f);
			}
		}

	}

	RuntimeAnimatorController  RandomSprite(RuntimeAnimatorController sprite)
	{
		int random = Random.Range(1, 4);

		Debug.Log(random);
		if (random == 1)
		{
			sprite = sprite1;
		}
		else if (random == 2)
		{
			sprite = sprite2;
		}
		else if (random == 3)
		{
			sprite = sprite3;
		}
		return sprite;

	}

}