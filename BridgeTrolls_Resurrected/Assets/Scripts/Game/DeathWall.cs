using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeathWall : MonoBehaviour {
	
	[SerializeField]private GameObject prefab1;
	[SerializeField]private GameObject prefab2;
	[SerializeField]private GameObject prefab3;
	[SerializeField]private GameObject Danger;
	[SerializeField]private GameObject Danger2;
	private bool Flip = false;
	private GameObject danger;
	private GameObject danger2;
	private Vector3 arrowX = new Vector3(0,0,0);
	private Vector2 dangerposition;
	private bool dangeronscreen = false;
	private int randomNummer;
	public float timer;
	[SerializeField, HideInInspector] private float _initialTime;

	public Text deathWallTimerText;

	//Instance
	public static DeathWall Instance {get; private set;}

	void Start()
	{
		_initialTime = 30;
		timer = _initialTime;
	}
	// Update is called once per frame
	void Update () {
		if (timer < 5 && dangeronscreen == false)
		{
			float startTime = Time.time;
			danger = Instantiate(Danger, dangerposition, transform.rotation) as GameObject;
			dangeronscreen = true;
		}
		if (timer > 0)
		{
			timer -= 1 * Time.deltaTime;
		}
		if (timer < 5)
		{
			arrowX = new Vector3(-10, 0, 0);
			dangerposition = new Vector2(-5.5f, 0);
			if (timer < 0)
			{
				Destroy(danger);
				danger2 = Instantiate(Danger2, dangerposition, transform.rotation) as GameObject;
				Flip = true;
				StartCoroutine(LeftToRight());  
				timer = _initialTime;
			}
		}
		else if (Input.GetKeyDown(KeyCode.KeypadEnter))
		{ 
			arrowX = new Vector3(12, 0, 0);
			Flip = false;
			StartCoroutine(RightToLeft());
		}

		deathWallTimerText.text = timer.ToString("F1");
    }

	IEnumerator LeftToRight()
	{
        Color dangercolor = danger2.GetComponentInChildren<SpriteRenderer>().color;
        yield return new WaitForSeconds(0.5f);
        for (float i = 1; i > 0; i -= 0.05f)
        {
            dangercolor = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.1f);
        }
		for (int k = 1; k < 20; k++)
		{
			arrowX += new Vector3(1, 0, 0);
			Vector3 arrowY = new Vector3(0, 0, 0);
			Vector3 arrow = new Vector3(0, 0, 0);
			for (int i = 0; i < 6; i++)
			{
				arrowY = new Vector3(0, Random.Range(-5.5f, 5.5f), 0);
				arrow = (arrowX + arrowY);
				StartTheDeathWall(arrow);
			}
			yield return new WaitForSeconds(1.2f);
		}
	}

	IEnumerator RightToLeft()
	{
		for (int k = 1; k < 20; k++)
		{
			arrowX -= new Vector3(1, 0, 0);
			Vector3 arrowY = new Vector3(0, 0, 0);
			Vector3 arrow = new Vector3(0, 0, 0);
			for (int i = 0; i < 6; i++)
			{
				arrowY = new Vector3(0, Random.Range(-5.5f, 5.5f), 0);
				arrow = (arrowX + arrowY);
				StartTheDeathWall(arrow);
			}
			yield return new WaitForSeconds(1.2f);
		}
	}


	void StartTheDeathWall(Vector3 HitLocation)
	{
		randomNummer = Random.Range(1,4);
		Debug.Log(randomNummer);
		if (Flip == false)
		{
			if (randomNummer == 1)
			{
				Instantiate(prefab1, HitLocation, transform.rotation);
				prefab1.GetComponent<SpriteRenderer>().flipX = false;
			}
			else if (randomNummer == 2)
			{
				Instantiate(prefab2, HitLocation, transform.rotation);
				prefab2.GetComponent<SpriteRenderer>().flipX = false;
			}
			else if (randomNummer == 3)
			{
				Instantiate(prefab3, HitLocation, transform.rotation);
				prefab3.GetComponent<SpriteRenderer>().flipX = false;
			}
		}
		else if (Flip == true)
		{
			if (randomNummer == 1)
			{
				Instantiate(prefab1, HitLocation, transform.rotation);
				prefab1.GetComponent<SpriteRenderer>().flipX = true;
			}
			else if (randomNummer == 2)
			{
				Instantiate(prefab2, HitLocation, transform.rotation);
				prefab2.GetComponent<SpriteRenderer>().flipX = true;
			}
			else if (randomNummer == 3)
			{
				Instantiate(prefab3, HitLocation, transform.rotation);
				prefab3.GetComponent<SpriteRenderer>().flipX = true;
			}
		}

	}

	public void Reset()
	{
		if(timer < _initialTime)
			timer = _initialTime;
	}
}