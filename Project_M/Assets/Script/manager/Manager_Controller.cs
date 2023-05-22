using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Controller : MonoBehaviour
{
    public Manager manager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manager.Manager_Update();
    }
}
