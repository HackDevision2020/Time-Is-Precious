using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BeeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(123);
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(456);
            transform.parent.gameObject.GetComponent<AIDestinationSetter>().enabled = true;
        }
    }
}
