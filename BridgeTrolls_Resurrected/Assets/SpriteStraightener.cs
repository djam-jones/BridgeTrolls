using UnityEngine;
using System.Collections;

public class SpriteStraightener : MonoBehaviour
{
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sprite.transform.rotation != Quaternion.identity)
        {
            sprite.transform.rotation = Quaternion.identity;
        }
    }
}
