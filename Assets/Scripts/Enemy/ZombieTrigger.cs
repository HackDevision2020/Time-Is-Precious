using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTrigger : MonoBehaviour
{
    public Transform risePosition;
    public GameObject zombiePrefab;
    private bool triggerOnce=true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && triggerOnce)
        {
            Debug.Log("123");
            GameObject zombie = Instantiate(zombiePrefab, risePosition.position, Quaternion.identity);
            zombie.transform.localScale = new Vector3(2, 2, 2);
            triggerOnce = false;
        }
    }


}
