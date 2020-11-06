using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public float stayTime = 3f;
    public float timer;

    private void FixedUpdate()
    {
        //move towards to player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.fixedDeltaTime);
        if (timer > stayTime)
        {
            Destroy(this.gameObject, 1f);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

    }
}
