using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IslandController : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //starts to fall
            GetComponent<SpriteRenderer>().DOFade(0, 2f);
            Destroy(gameObject, 2f);
            
        }
    }
}
