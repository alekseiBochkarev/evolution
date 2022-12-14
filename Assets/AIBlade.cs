using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIBlade : AI
{
    public static int[] skillsTotal = new int[4];

    //public GameObject bacteriumPrefab;

    //public int foodSkill = 0;
    //public int attackSkill = 0;
    //public int defSkill = 0;
    //public float energy = 10;
    //public float age = 0;
    float damage;

    Color col;
    float size;

    private static int inputsCount = 4;
    private Genome genome;
    private NN nn;
    private float vision;
    private static float[] inputs = new float[inputsCount];
    Collider2D[] colliders;
    private static float[] neighboursCount = new float[4];
    Vector3[] vectors = new Vector3[4];
    MainController mainController;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        mainController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainController>();
    }


    void Update()
    {
        RotateEnemy();
        AgeCalculation();

        FindGoalToMove();


        Move(nn, inputs, neighboursCount, vectors, vision);
        LifeEnergyCalculate();

    }

    public override void FindGoalToMove()
    {
        vision = 5f + attackSkill;

        colliders = Physics2D.OverlapCircleAll(transform.position, vision);

        //neighboursCount = new float[4];
        // вектора к центрам масс еды, красного, зеленого и синего
        vectors = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            neighboursCount[i] = 0;
            vectors[i] = new Vector3(0f, 0f, 0f);
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == gameObject) continue;
            if (colliders[i].gameObject.name == "food")
            {
                neighboursCount[0]++;
                vectors[0] += colliders[i].gameObject.transform.position - transform.position;
            }
            //else if (Equals(colliders[i].gameObject.name, "bacterium") || Equals(colliders[i].gameObject.name, "bacteriumPlayer"))
            //else if (!Equals("food", colliders[i].gameObject.name) & !Equals("wall", colliders[i].gameObject.name) & !Equals("bullet", colliders[i].gameObject.name))
            if (colliders[i].gameObject.CompareTag("Player"))
            {

                AI ai = colliders[i].gameObject.GetComponent<AI>();
                    neighboursCount[1] += ai.attackSkill / 3f;
                    vectors[1] += (colliders[i].gameObject.transform.position - transform.position) * ai.attackSkill;
                    neighboursCount[2] += ai.foodSkill / 3f;
                    vectors[2] += (colliders[i].gameObject.transform.position - transform.position) * ai.foodSkill;
                    neighboursCount[3] += ai.defSkill / 3f;
                    vectors[3] += (colliders[i].gameObject.transform.position - transform.position) * ai.defSkill;
                
            }
        }
    }

    public override void Move(NN nn, float[] inputs, float[] neighboursCount, Vector3[] vectors, float vision)
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
        velocity += target * (0.25f + attackSkill * 0.05f);
        velocity *= 0.98f;
        rb.velocity = velocity;
    }

    public override void AgeCalculation()
    {
        age += Time.deltaTime;
        if (age > mainController.playerMaxAge) {
            mainController.playerMaxAge = age;
        }
    }

    public override void LifeEnergyCalculate()
    {
        // float antibiotics = 1f;
        // концентрация антибиотиков
        // if(transform.position.x < -39) antibiotics = 4;
        // else if(transform.position.x < -20) antibiotics = 3;
        // else if(transform.position.x < -1) antibiotics = 2;
        // antibiotics = Mathf.Max(1f, antibiotics - defSkill);
        energy -= Time.deltaTime; //* antibiotics * antibiotics;
        if (energy < 0f)
        {
            Dead();
        }
    }

    public void CollisionFromChild(Collider2D col)
    {

    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (foodSkill == 0) return;
        if (Equals("food", col.gameObject.name))
        {
            Eat(foodSkill);
            Destroy(col.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (age < 1f) return;
        if (attackSkill == 0) return;
        //if (Equals(col.gameObject.name, "bacterium") || Equals(col.gameObject.name, "bacteriumPlayer"))
        //if (!Equals("food", col.gameObject.name) & !Equals("wall", col.gameObject.name))
        if (col.gameObject.CompareTag("Player"))
        {
            //Debug.Log(col.gameObject.name);
            //if (Equals(col.gameObject.name, "bacterium"))
            if (!Equals(command, col.gameObject.GetComponent<AI>().command))
            {
                if (col.gameObject.GetComponent<AI>().age < 1f) return;
                Attack(col);

            }
        }
    }

    public override void Attack(Collision2D col)
    {
        //урон - максимальное из 0 либо атака текущего - защита противоположного юнита
        damage = Mathf.Max(0f, attackSkill - col.gameObject.GetComponent<AI>().defSkill);
        damage *= 4f;
        damage = Mathf.Min(damage, col.gameObject.GetComponent<AI>().energy);
        col.gameObject.GetComponent<AI>().energy -= damage;
        Eat(damage);
    }

    public override void RotateEnemy()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90);
    }

    public override void Init(Genome g)
    {
        genome = g;
        //col = new Color(0.1f, 0.1f, 0.25f, 1f);
        size = 0.75f;
        /*
        //вот это нужно будет полностью переработать. сейчас завязка на характеристики (нужно будет сделать подругому)
        // for (int i = 0; i < Genome.skillCount; i++)
        for (int i = 0; i < g.skillCount; i++)
        {
            //for what?
            skillsTotal[g.skills[i]]++;
            //too strange characteristic init
            if (g.skills[i] == 0)
            {
                foodSkill++;
                col.g += 0.2f;
            }
            else if (g.skills[i] == 1)
            {
                attackSkill++;
                col.r += 0.25f;
            }
            else if (g.skills[i] == 2)
            {
                defSkill++;
                col.b += 0.25f;
            }
            else if (g.skills[i] == 3)
            {
                size += 0.5f;
            }
        }
        */
        transform.localScale = new Vector3(size, size, size);
        //old
        //gameObject.GetComponent<SpriteRenderer>().color = col;
        //new
        switch (command)
        {
            case 1:
                {
                    gameObject.GetComponent<SpriteRenderer>().color = UnityEngine.Color.blue;
                    break;
                }
            case 2:
                {
                    gameObject.GetComponent<SpriteRenderer>().color = UnityEngine.Color.red;
                    break;
                }
            case 0:
                {
                    gameObject.GetComponent<SpriteRenderer>().color = UnityEngine.Color.magenta;
                    break;
                }
        }

        //здесь какие то установки для нейронной сети (weight это похоже веса для нейросети) - depend on moving
        nn = new NN(inputsCount, 8, 4);
        for (int i = 0; i < inputsCount; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                nn.layers[0].weights[i, j] = genome.weights[i + j * inputsCount];
            }
        }
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                nn.layers[1].weights[i, j] = genome.weights[i + j * 8 + inputsCount * 8];
            }
        }
    }



    public override void Dead()
    {
        for (int i = 0; i < genome.skillCount; i++)
        {
            skillsTotal[genome.skills[i]]--;
        }
        switch (command)
        {
            case 1:
            {
                    mainController.playerCount--;
                    break;
                }
            case 2:
            {
                    mainController.m1Count--;
                    break;
                }
        }
        Destroy(gameObject);
        //mainController.playerCount--;
    }

    public override void Eat(float food)
    {
        energy += food;
        if (energy > 16)
        {
            energy *= 0.5f;
            CreateNewLife(energy);
        }
    }



    public override void CreateNewLife(float energy)
    {
        GameObject b = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(name, typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);
        try
        {
            
            b.transform.position = transform.position;
            b.name = name;
            Genome g = new Genome(genome);
            //при смене значения меняется поведение Мутате (0,5 или 1, или 2) - скорость движения, скорость размножения, кучкование и чем больше значение тем меньше останется зеленых
            g.Mutate(0.5f);
            AI ai = b.GetComponent<AI>();
            ai.command = command;
            ai.Init(g);
            ai.energy = energy;
            switch (command)
            {
                case 1:
                {
                        mainController.playerCount++;
                        break;
                    }
                case 2:
                {
                        mainController.m1Count++;
                        break;
                    }
            }
            //mainController.playerCount++;
        } catch (NullReferenceException e)
        {
            Debug.Log(e);
        }
        
    }

}

