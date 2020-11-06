using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animator anim;
    private bool startChasing;
    public GameObject player;
    public float speed = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (startChasing)
        {
            //start to chase player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.fixedDeltaTime);
        }
    }

    private void StartToChase()
    {
        anim.SetBool("startChasing", true);
        startChasing = true;
    }
}
