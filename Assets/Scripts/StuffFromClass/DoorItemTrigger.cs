using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItemTrigger : MonoBehaviour
{
    [SerializeField] private GameObject door; 
    private static DoorItemTrigger _instance;
    public static DoorItemTrigger Instance { get { return _instance; } }
    // Start is called before the first frame update
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private int totalItems = 0, itemsCollected = 0;
    void Start()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        Debug.Log(totalItems);
    }
    public void PickedUpItem()
    {
        itemsCollected++;
        Debug.Log("Items collected " + itemsCollected);
    }
    // Update is called once per frame
    void Update()
    {
        if(totalItems == itemsCollected)
        {
            Destroy(door);
        }
    }
}
