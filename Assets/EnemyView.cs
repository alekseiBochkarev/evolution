using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : EvoElement
{
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    void Start()
    {
      app.model.enemy.rb = gameObject.GetComponent<Rigidbody2D>();

    }
}
