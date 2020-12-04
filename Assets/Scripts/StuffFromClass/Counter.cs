using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    // Start is called before the first frame update
    public static int count = 0;
    public int limit = 1;
    public GameObject ghost;
    public GameObject effect;


    // Update is called once per frame
    void Update()
    {
        if (count == limit)
        {
            GameObject.Instantiate(effect, this.transform.position, this.transform.rotation);
            Destroy(ghost, 1);
        }
    }
}
