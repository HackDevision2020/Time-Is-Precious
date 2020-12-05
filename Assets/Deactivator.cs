using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public int timer;
    public GameObject obj;
    public GameObject ghost;

    public void Start()
    {
        Destroy(obj, timer);
        ghost.SetActive(false);
        Invoke("Ghost", timer - 1);



    }

    public void ClosePannel()
    {
        Destroy(obj, 1);
        ghost.SetActive(true);
        
    }

    IEnumerable RemoveAfterSeconds()
    {
        yield return new WaitForSeconds(timer);
        ghost.SetActive(true);
    }

    public void Ghost()
    {
        ghost.SetActive(true);
    }
}
