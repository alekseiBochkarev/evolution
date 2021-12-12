using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _speed;
    private Vector2 _target;
    

    public void StartMovingTowards(Vector2 target, float speed)
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _target = target;
        _speed = speed;
    }

    public void Move()
    {
        Vector2 velocity = rb.velocity;
        velocity += _target * (_speed) * 0.01f;
        rb.velocity = velocity;
    }

    public void FixedUpdate()
    {
        // Speed will be 0 before StartMovingTowards is called so this will do nothing
        Move();
        //in future we will setActive (false)
        Destroy(gameObject, 1f);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // Forward to the parent (or just deal with it here).
        // Let's say it has a script called "PlayerCollisionHelper" on it:
        //AI parentScript = transform.parent.GetComponent<AI>();

        // Let it know a collision happened:
        transform.parent.GetComponent<AI>().CollisionFromChild(col);
        Destroy(this.gameObject);

    }


}
