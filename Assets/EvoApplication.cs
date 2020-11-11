using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoApplication : MonoBehaviour
{
    //reference to the root instances of the MVC
    public EvoModel model;
    public EvoView view;
    public EvoController controller;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}

public class EvoElement : MonoBehaviour
{
    //gives access to the application and all instances
    public EvoApplication app { get { return GameObject.FindObjectOfType<EvoApplication>(); } }
}
