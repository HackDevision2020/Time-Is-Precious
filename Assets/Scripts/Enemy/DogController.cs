using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DogController : MonoBehaviour
{
    public float completeTime;
    public float distance;

    private void Start()
    {
        transform.DOMoveX(transform.position.x + distance, completeTime).OnComplete(GoBackwards).SetEase(Ease.Unset);
    }

    private void GoBackwards()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        transform.DOMoveX(transform.position.x - distance, completeTime).OnComplete(GoForwards).SetEase(Ease.Unset);
    }
    private void GoForwards()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        transform.DOMoveX(transform.position.x + distance, completeTime).OnComplete(GoBackwards).SetEase(Ease.Unset);
    }

}
