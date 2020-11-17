using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : EvoElement
{

    public Rigidbody2D rb;
   


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    void Update()
    {
        RotateEnemy();
        gameObject.GetComponent<EnemyController>().AgeCalculation();

        gameObject.GetComponent<EnemyController>().FindGoalToMove();


        Move(gameObject.GetComponent<EnemyModel>().nn, gameObject.GetComponent<EnemyModel>().inputs, gameObject.GetComponent<EnemyModel>().neighboursCount, gameObject.GetComponent<EnemyModel>().vectors, gameObject.GetComponent<EnemyModel>().vision);
        gameObject.GetComponent<EnemyController>().LifeEnergyCalculate();

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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.GetComponent<EnemyModel>().age < 1f) return;
        if (gameObject.GetComponent<EnemyModel>().attackSkill == 0) return;
        if (Equals(col.gameObject.name, "bacterium"))
        {
            if (col.gameObject.GetComponent<AI>().age < 1f) return;
            gameObject.GetComponent<EnemyController>().Attack(col);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.GetComponent<EnemyModel>().foodSkill == 0) return;
        if (col.gameObject.name == "food")
        {
            gameObject.GetComponent<EnemyController>().Eat(gameObject.GetComponent<EnemyModel>().foodSkill);
            Destroy(col.gameObject);
        }
    }

    void Move(NN nn, float[] inputs, float[] neighboursCount, Vector3[] vectors, float vision)
    {
        for (int i = 0; i < 4; i++)
        {
            if (neighboursCount[i] > 0)
            {
                vectors[i] /= neighboursCount[i] * vision;
                inputs[i] = vectors[i].magnitude;
            }
            else
            {
                inputs[i] = 0f;
            }
        }
        float[] outputs = nn.FeedForward(inputs);
        Vector2 target = new Vector2(0, 0);
        for (int i = 0; i < 4; i++)
        {
            if (neighboursCount[i] > 0)
            {
                Vector2 dir = new Vector2(vectors[i].x, vectors[i].y);
                dir.Normalize();
                target += dir * outputs[i];
            }
        }
        if (target.magnitude > 1f) target.Normalize();
        Vector2 velocity = rb.velocity;
        velocity += target * (0.25f + gameObject.GetComponent<EnemyModel>().attackSkill * 0.05f);
        velocity *= 0.98f;
        rb.velocity = velocity;
    }

    public void CreateNewLife(float energy)
    {

        Vector3 vector3 = new Vector3(0, 0, 0);
        GameObject b = app.controller.CreateLife(vector3);

        //GameObject b = (GameObject)Object.Instantiate(Resources.Load("m1", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);
        //b.transform.position = transform.position;
        //b.name = "bacterium";
        Genome g = new Genome(gameObject.GetComponent<EnemyModel>().genome);
        //при смене значения меняется поведение Мутате (0,5 или 1, или 2) - скорость движения, скорость размножения, кучкование и чем больше значение тем меньше останется зеленых
        g.Mutate(0.5f);

        b.GetComponent<EnemyModel>().Init(g);
        b.GetComponent<EnemyModel>().energy = energy;
        //AI ai = b.GetComponent<AI>();
        //ai.Init(g);
        //ai.energy = energy;
    }

}
