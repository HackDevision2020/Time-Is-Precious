using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonManager : MonoBehaviour
{
    public List<Transform> wayPoints;

    private int currentTargetIndex;
    public float speed = 5f;

    private bool isFacingRight;
    private bool movingRight;
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        currentTargetIndex = 0;
        isFacingRight = true;
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentTargetIndex].position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, wayPoints[currentTargetIndex].position) < 0.01f)
        {
            //close Enough change my target
            //for(int i = 0; i < wayPoints.Count; i++)
            currentTargetIndex = (currentTargetIndex + 1) % wayPoints.Count;
            Flip();


        }

    }
    private void Flip()
    {
        // Flip player
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }
}
