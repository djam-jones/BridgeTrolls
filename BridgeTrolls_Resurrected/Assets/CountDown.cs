using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private Text TextObject;
    private string text;
    private float timer = 3;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TextObject.text = text;
        text = timer.ToString("f0");
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
    }
}
