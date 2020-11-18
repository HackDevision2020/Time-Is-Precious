using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatController : MonoBehaviour
{
    private SpriteRenderer sr;
    private float alpha = 1;
    private bool startToFade;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (startToFade)
        {
            alpha -= 0.002f;
            sr.color = new Color(255, 255, 255, alpha);
            if (alpha < 0.01f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            startToFade = true;
        }
    }
}
