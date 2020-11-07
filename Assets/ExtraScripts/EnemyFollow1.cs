using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow1 : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;
    [SerializeField] float stoppingDistance = 3;
    //[SerializeField] private int enemyHealth = 100;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rbody;

    //[SerializeField] GameObject[] deathSplatters;
    //[SerializeField] private GameObject hitEffect;

    //[SerializeField] private bool shouldShoot;
    //[SerializeField] private GameObject bullet;
    //[SerializeField] private Transform firePoint;
    //[SerializeField] float fireRate;
   // private float fireCounter;

    [SerializeField] public float tooFar = 3;
    

    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // target object with "Player" tag
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < tooFar)
        {
           

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);  // move towards
          

            if ((isFacingRight && rbody.velocity.x < 0) || (!isFacingRight && rbody.velocity.x > 0))
             {
                 Flip();
                 //transform.Rotate(0f, 180f, 0f);
                 Debug.Log("Trigger");
             }


            if (target.position.x > transform.position.x && !isFacingRight)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFacingRight = true;

            }
            else if (target.position.x < transform.position.x && isFacingRight)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFacingRight = false;
            }



            /*if (shouldShoot == true && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < tooFar)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                    AudioManager.instance.Playsfx(1);
                }
            }*/
        }


        /*if (transform.position.x > 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }*/

    }

    private void Flip()
    {
        //transform.Rotate(0f, 180f, 0f);
        
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
       isFacingRight = !isFacingRight;

    }

    /*public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        Instantiate(hitEffect, transform.position, transform.rotation);

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);

            int selectSpaltter = Random.Range(0, deathSplatters.Length);

           Instantiate(deathSplatters[selectSpaltter], transform.position, transform.rotation);
        }
    }*/
}
