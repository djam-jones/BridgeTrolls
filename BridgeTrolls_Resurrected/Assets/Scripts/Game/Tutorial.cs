using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject[] screens;
    [SerializeField]
    private float slideSpeed = 3;
    [SerializeField]
    private int selectedSlide = 0;
    [SerializeField]
    private int targetSlide;
    private float slideDistance = 17.5f;

    private bool sliding = false;

    void Start()
    {
        makeSlides();
        slideToSlide(2);
    }

    void Update()
    {
        if (this.transform.position.x > -targetSlide * slideDistance)
        {
            this.transform.Translate(Vector2.left * Time.deltaTime * slideSpeed, 0);
        }
        else if (this.transform.position.x + 1 < -targetSlide * slideDistance)
        {
            this.transform.Translate(Vector2.right * Time.deltaTime * slideSpeed, 0);
        }
    }

    private void makeSlides()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            GameObject newSlide = Instantiate(screens[i], new Vector2(i * slideDistance, 0), Quaternion.identity) as GameObject;
            newSlide.transform.parent = this.gameObject.transform;
        }
    }

    public void slideToSlide(int target)
    {
        targetSlide = target;
        selectedSlide = target;
    }
}
