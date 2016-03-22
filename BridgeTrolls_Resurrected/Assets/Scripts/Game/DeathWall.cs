using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeathWall : MonoBehaviour {

	private RuntimeAnimatorController animations;
    private GameObject newarrow;
    private List<GameObject> arrowlist = new List<GameObject>();

	[SerializeField]private GameObject arrow;
    [SerializeField]private GameObject StartWallAnimation;
    [SerializeField]private GameObject AfterWallAnimation;

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
			StartCoroutine(spawnArrowsToRight());
		}

        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(spawnArrowsToLeft());
        }
	}
    IEnumerator spawnArrowsToLeft()
    {

        GameObject newDangerAnim = Instantiate(StartWallAnimation, new Vector2(5.81f, 0), transform.rotation) as GameObject;
        yield return new WaitForSeconds(3f);
        Destroy(newDangerAnim);
        GameObject newDangerAnimAfter = Instantiate(AfterWallAnimation, new Vector2(5.81f, 0), transform.rotation) as GameObject;

        SpriteRenderer[] dangercolor = newDangerAnimAfter.GetComponentsInChildren<SpriteRenderer>();
        yield return new WaitForSeconds(0.2f);

        for (float i = 1; i > 0; i -= 0.05f)
        {
            dangercolor[0].color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);

            if (i < 0.1f)
            {
                Destroy(newDangerAnimAfter);
            }
        }

        for (int x = 10; x >= -10; x--)
        {
            for(int y = 5; y>= -6; y--)
            {
                Vector2 position = new Vector2(x + Random.Range(0.5f, -0.5f), y + Random.Range(0.5f, -0.5f));
                if (position.y > 5.07)
                {
                    position.y = 4.9f;
                }
                if (position.x > 10)
                {
                    position.x = 10;
                }
                else if (position.x < -10)
                {
                    position.x = -10;
                }
                arrow.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)(RandomSprite(animations));
                newarrow = Instantiate(arrow, position, transform.rotation) as GameObject;
                arrowlist.Add(newarrow);
                newarrow.GetComponent<SpriteRenderer>().flipX = false;
                newarrow.GetComponent<SpriteRenderer>().sortingOrder = ((int)newarrow.transform.position.y * -1) + 7;
                yield return new WaitForSeconds(0.01f);
            }
        }
        StartCoroutine(removeArrows());
    }
	IEnumerator spawnArrowsToRight()
	{
        GameObject newDangerAnim = Instantiate(StartWallAnimation, new Vector2(-5.88f, 0), transform.rotation) as GameObject;
        yield return new WaitForSeconds(3f);
        Destroy(newDangerAnim);
        GameObject newDangerAnimAfter = Instantiate(AfterWallAnimation, new Vector2(-5.88f, 0), transform.rotation) as GameObject;

        SpriteRenderer[] dangercolor = newDangerAnimAfter.GetComponentsInChildren<SpriteRenderer>();
        yield return new WaitForSeconds(0.2f);

        for (float i = 1; i > 0; i -= 0.05f)
        {
            dangercolor[0].color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);

            if (i < 0.1f)
            {
                Destroy(newDangerAnimAfter);
            }
        }

        for (int x = -10; x <= 10; x += 1)
		{

			for (int y = -6; y <=  5; y += 1)
			{
                Vector2 position = new Vector2(x + Random.Range(0.5f, -0.5f), y + Random.Range(0.5f, -0.5f));
                if (position.y > 5.07)
                {
                    position.y = 4.9f;
                }
                if (position.x > 10)
                {
                    position.x = 10;
                }
                else if (position.x < -10)
                {
                    position.x = -10;
                }
                arrow.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)(RandomSprite(animations));
				newarrow = Instantiate(arrow, position, transform.rotation) as GameObject;
                arrowlist.Add(newarrow);
                newarrow.GetComponent<SpriteRenderer>().flipX = true;
                newarrow.GetComponent<SpriteRenderer>().sortingOrder = ((int)newarrow.transform.position.y *-1) + 7;
				yield return new WaitForSeconds(0.01f);
			}
		}
        StartCoroutine(removeArrows());
	}

    IEnumerator removeArrows()
    {
        for (float i = 1; i > 0; i -= 0.05f)
        {
            for (int j = 0; j < arrowlist.Count; j++)
            { 
                arrowlist[j].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            }
            yield return new WaitForSeconds(0.1f);

            if (i < 0.1f)
            {
                foreach(GameObject arrow in arrowlist)
                {
                    Destroy(arrow);
                }
                arrowlist.Clear();
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