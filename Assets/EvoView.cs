using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Contains all views related to the app
public class EvoView : EvoElement
{
    void Start()
    {
    
        app.controller.StartEvolution();
        
    }
}
