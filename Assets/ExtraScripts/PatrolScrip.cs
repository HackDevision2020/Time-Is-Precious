using Controllers.Movement;
using UnityEngine;

[DisallowMultipleComponent]
public class PatrolScrip : MonoBehaviour
{
    //This is a standalone patrol scrit add this to an object and add the sensor (empty gameObject in the inspector that is child of the primary object)

    public bool simpleMovement = true;
    public float speed = 2f;
    private bool movingRight = true;
    public Transform groundDetector;

    private CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!simpleMovement)
        {
            return;
        }

        Vector3 movement = movingRight ? Vector3.right : Vector3.left;
        Vector3 newPosition = transform.position + movement * (speed * Time.deltaTime);
        transform.position = newPosition;

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 2f);
        if (!groundInfo.collider)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (simpleMovement)
        {
            return;
        }

        Vector2 forward = movingRight ? Vector2.right : Vector2.left;
        float movement = speed * Time.fixedDeltaTime;

        characterMovement.Move(movement * forward, false);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, 2f);
        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetector.position, forward, 0.2f);

        if (!groundInfo.collider || wallInfo.collider)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (movingRight)
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
