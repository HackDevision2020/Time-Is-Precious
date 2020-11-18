using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour
{
    private Animator anim;
    public float attackDuration;
    public float totalDuration;
    private float timer;
    public GameObject attackGo;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer < attackDuration)
        {
            //witch attack
            timer += Time.deltaTime;
            anim.SetBool("isAttack", true);
            attackGo.SetActive(true);
        }
        else if(timer > attackDuration && timer < totalDuration)
        {
            //witch idle
            timer += Time.deltaTime;
            anim.SetBool("isAttack", false);
            attackGo.SetActive(false);
        }
        else
        {
            timer = 0;
        }
    }
}
