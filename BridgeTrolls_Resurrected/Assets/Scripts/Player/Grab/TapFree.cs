using UnityEngine;
using System.Collections;

public class TapFree : MonoBehaviour
{
    [SerializeField]
    private GameObject bar;
    [SerializeField]
    private float strength = 3;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bar.transform.localScale += new Vector3(1, 0, 0);
        }

        if(bar.transform.localScale.x > 0)
        {
            bar.transform.localScale -= new Vector3(1 * Time.deltaTime * strength, 0, 0);
        }
    }
}