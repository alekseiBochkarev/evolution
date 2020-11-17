using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : EvoElement
{
    public delegate void PositionEvent(Vector3 position);
    public event PositionEvent OnPositionChanged;

    private int[] _skillsTotal = new int[4];
    public int [] skillsTotal
    {
        get
        {
            return _skillsTotal;
        }
        set
        {
            if(_skillsTotal != value)
            {
                _skillsTotal = value;
            }
        }
    }

    private int _foodSkill = 0;
    public int foodSkill
    {
        get
        {
            return _foodSkill;
        }
        set
        {
            if(_foodSkill != value)
            {
                _foodSkill = value;
            }
        }
    }

    private int _attackSkill = 0;
    public int attackSkill
    {
        get
        {
            return _attackSkill;
        }
        set
        {
            if (_attackSkill != value)
            {
                _attackSkill = value;
            }
        }
    }
    private int _defSkill = 0;
    public int defSkill
    {
        get
        {
            return _defSkill;
        }
        set
        {
            if (_defSkill != value)
            {
                _defSkill = value;
            }
        }
    }
    private float _energy = 10;
    public float energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
        }
    }
    private float _age = 0;
    public float age
    {
        get
        {
            return _age;
        }
        set
        {
            _age = value;
        }
    }

    private float _damage;
    public float damage
    {
        get
        {
            return _damage;
        }
        set
        {
            if (_damage != value)
            {
                _damage = value;
            }
        }
    }
    public Color col;
   

    private float _size;
    public float size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value;
        }
    }

    private static int _inputsCount = 4;
    public static int inputsCount
    {
        get
        {
            return _inputsCount;
        }
        set
        {
            _inputsCount = value;
        }
    }

    private Genome _genome;
    public Genome genome
    {
        get
        {
            return _genome;
        }
        set
        {
            _genome = value;
        }
    }

    private NN _nn;
    public NN nn
    {
        get
        {
            return _nn;
        }
        set
        {
            _nn = value;
        }
    }

    private float _vision;
    public float vision
    {
        get
        {
            return _vision;
        }
        set
        {
            _vision = value;
        }
    }

    
    public float [] inputs = new float[inputsCount];
    
    public float [] neighboursCount = new float[4];
   

    private Vector3[] _vectors = new Vector3[4];
    public Vector3 [] vectors
    {
        get
        {
            return _vectors;
        }
        set
        {
            _vectors = value;
        }
    }

    


    //пока не использую, буду использовать для движения 8965 023 10 82
    private Vector3 _position;
    public Vector3 position
    {
        get
        {
            return _position;
        }
        set
        {
            if (_position != value)
            {
                _position = value;
                if (OnPositionChanged !=null)
                {
                    OnPositionChanged(value);
                }
            }
        }
    }

    public void Init(Genome g)
    {
        genome = g;
        col = new Color(0.1f, 0.1f, 0.25f, 1f);
        size = 0.75f;
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

        gameObject.GetComponent<EnemyView>().SetSize(size);
        //transform.localScale = new Vector3(size, size, size);
        gameObject.GetComponent<EnemyView>().SetColor(col);
        //gameObject.GetComponent<SpriteRenderer>().color = col;

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



}
