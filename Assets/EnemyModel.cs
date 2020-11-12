using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    public delegate void PositionEvent(Vector3 position);
    public event PositionEvent OnPositionChanged;

    private static int[] _skillsTotal = new int[4];
    public static int [] skillsTotal
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
    private Color _col;
    public Color col
    {
        get
        {
            return _col;
        }
        set
        {
            _col = value;
        }
    }

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

    private static float[] _inputs = new float[inputsCount];
    public static float [] inputs
    {
        get
        {
            return _inputs;
        }
        set
        {
            _inputs = value;
        }
    }


    private Collider2D[] _colliders;
    public Collider2D[] colliders
    {
        get
        {
            return _colliders;
        }
        set
        {
            _colliders = value;
        }
    }

    private static float[] _neighboursCount = new float[4];
    public static float [] neighboursCount
    {
        get
        {
            return _neighboursCount;
        }
        set
        {
            _neighboursCount = value;
        }
    }

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

    

}
