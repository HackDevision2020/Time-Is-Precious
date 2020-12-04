using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyItems : MonoBehaviour
{
    [SerializeField] private GameObject itemFeedback;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //referenc to levelController
        if (other.CompareTag("Player"))
        {
            Counter.count++;
            //play pickup animation
            GameObject.Instantiate(itemFeedback, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
