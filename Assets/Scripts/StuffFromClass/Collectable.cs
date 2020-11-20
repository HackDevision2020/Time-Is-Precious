using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject itemFeedback;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //referenc to levelController
        if(other.CompareTag("Player"))
        {
            //play pickup animation
            GameObject.Instantiate(itemFeedback, this.transform.position, this.transform.rotation);

            //Increase player item pickup counter
            DoorItemTrigger.Instance.PickedUpItem();
            Destroy(this.gameObject);
        }
    }
}
