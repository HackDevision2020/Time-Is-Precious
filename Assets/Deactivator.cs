using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public int timer = 4;
    public GameObject obj;

    public void Start()
    {
        Destroy(obj, 4);
    }

    IEnumerable RemoveAfterSeconds()
    {
        yield return new WaitForSeconds(timer);
        obj.SetActive(false);
    }
}
