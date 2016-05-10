using UnityEngine;
using System.Collections;

public class SpriteStraightener : MonoBehaviour
{
    private SpriteRenderer sprite;

    void Update()
    {
        if(this.transform.rotation != Quaternion.identity)
        {
            this.transform.rotation = Quaternion.identity;
        }
    }
}
