using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathWall : MonoBehaviour {
    [SerializeField]private GameObject prefab1;
    [SerializeField]private GameObject prefab2;
    [SerializeField]private GameObject prefab3;
    [SerializeField]private GameObject Danger;
    private Vector3 arrowX = new Vector3(0,0,0);
    private Vector2 dangerposition;
    private bool dangeronscreen = false;
    private int randomNummer;
    [SerializeField]private float timer;

    void Start()
    {
        timer = 60;

    }
    // Update is called once per frame
    void Update () {
        if (timer < 5 && dangeronscreen == false)
        {
            float startTime = Time.time;
            Instantiate(Danger, dangerposition, transform.rotation);
            dangeronscreen = true;
        }
        if (timer < 2)
        {
            Debug.Log("opzich");
            Danger.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1, Mathf.Lerp(0, 1, Time.time));
            
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
                StartCoroutine(LeftToRight());
                timer = 60;
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            arrowX = new Vector3(10, 0, 0);
            StartCoroutine(RightToLeft());
        }

    }
    IEnumerator LeftToRight()
    {
        for (int k = 1; k < 20; k++)
        {
            arrowX += new Vector3(1, 0, 0);
            Vector3 arrowY = new Vector3(0, 0, 0);
            Vector3 arrow = new Vector3(0, 0, 0);
            for (int i = 0; i < 6; i++)
            {
                arrowY = new Vector3(0, Random.Range(-4.5f, 5.5f), 0);
                arrow = (arrowX + arrowY);
                StartTheDeathWall(arrow);
            }
            yield return new WaitForSeconds(1);
        }

    }

    IEnumerator RightToLeft()
    {
        for (int k = 1; k < 19; k++)
        {
            arrowX -= new Vector3(1, 0, 0);
            Vector3 arrowY = new Vector3(0, 0, 0);
            Vector3 arrow = new Vector3(0, 0, 0);
            for (int i = 0; i < 6; i++)
            {
                arrowY = new Vector3(0, Random.Range(-4.5f, 5.5f), 0);
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
        if (randomNummer == 1)
        {
            Instantiate(prefab1, HitLocation, transform.rotation);
        }
        else if(randomNummer == 2)
        {
            Instantiate(prefab2, HitLocation, transform.rotation);
        }
        else if (randomNummer == 3)
        {
            Instantiate(prefab3, HitLocation, transform.rotation);
        }

    }
}