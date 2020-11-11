using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    public delegate void PositionEvent(Vector3 position);
    public event PositionEvent OnPositionChanged;

    public static int[] skillsTotal = new int[4];
    public int foodSkill = 0;
    public int attackSkill = 0;
    public int defSkill = 0;
    public float energy = 10;
    public float age = 0;
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

    public Rigidbody2D rb { get; set; };

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

    private Vector3 _position;

}
