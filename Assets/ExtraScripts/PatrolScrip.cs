using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScrip : MonoBehaviour
{
  //This is a standalone patrol scrit add this to an object and add the sensor (empty gameObject in the inspector that is child of the primary object)

    public float speed = 2f;
    private bool movingRight = true;
    public Transform groundDetector;  

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 2f);

        if(groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
