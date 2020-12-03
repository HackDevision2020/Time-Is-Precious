using System.Collections.Generic;
using Controllers.Movement;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterMovement))]
public class skeletonManager : MonoBehaviour
{
    public List<Transform> wayPoints;

    private int currentTargetIndex;
    public float speed = 5f;
    public float arriveThreshold = 0.01f;

    private bool paused;

    private bool isFacingRight;
    private bool movingRight;
    private CharacterMovement characterMovement;
    private Rigidbody2D rBody;

    public void SetPause(bool shouldPause)
    {
        paused = shouldPause;
    }

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTargetIndex = 0;
        isFacingRight = true;
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (paused)
        {
            return;
        }

        float movement = speed * Time.fixedDeltaTime;

        if (Mathf.Abs(rBody.position.x - wayPoints[currentTargetIndex].position.x) < arriveThreshold)
        {
            currentTargetIndex = (currentTargetIndex + 1) % wayPoints.Count;
        }

        if (wayPoints[currentTargetIndex].position.x - rBody.position.x < 0.0f)
        {
            movement *= -1.0f;
        }

        characterMovement.Move(new Vector2(movement, 0.0f), false);

        if (movement > 0.0f && !isFacingRight || movement < 0.0f && isFacingRight)
        {
            Flip();
        }

        // transform.position = Vector2.MoveTowards(
        //     transform.position,
        //     wayPoints[currentTargetIndex].position,
        //     Time.fixedDeltaTime * speed);
        //
        // if (Vector2.Distance(transform.position, wayPoints[currentTargetIndex].position) < 0.01f)
        // {
        //     //close Enough change my target
        //     //for(int i = 0; i < wayPoints.Count; i++)
        //     currentTargetIndex = (currentTargetIndex + 1) % wayPoints.Count;
        //     Flip();
        // }
    }

    private void Flip()
    {
        // Flip player
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }
}
