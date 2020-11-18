using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BeeController : MonoBehaviour
{
    public AIPath aiPath;

    private void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            //moving to right
            transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);

        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            //moving to left
            transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
    }
}
