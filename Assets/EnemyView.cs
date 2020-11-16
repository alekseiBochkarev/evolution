using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : EvoElement
{
    private Rigidbody2D _rb;
    public Rigidbody2D rb
    {
        get
        {
            return _rb;
        }
        set
        {
            rb = value;
        }
    }


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    

    private void RotateEnemy()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90);
    }

    public void SetSize (float size)
    {
        transform.localScale = new Vector3(size, size, size);
        
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

}
