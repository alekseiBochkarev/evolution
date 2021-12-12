using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour, IMove, IEat
{
    public static int[] skillsTotal = new int[4];

    //public GameObject bacteriumPrefab;
    public int command;
    public int foodSkill;
    public int attackSkill;
    public int defSkill;
    public float energy;
    public float age;
    
    //float damage;

    //Color col;
    //float size;

    //static int inputsCount;
    //Genome genome;
    //NN nn;
    //float vision;
    //static float[] inputs;
    //Collider2D[] colliders;
    //static float[] neighboursCount;
    //Vector3[] vectors;

    //Rigidbody2D rb;

    // Start is called before the first frame update


    public virtual void FindGoalToMove ()
    {
       
    }

    public virtual void Move(NN nn, float[] inputs, float[] neighboursCount, Vector3[] vectors, float vision)
    {
        
    }

    public virtual void AgeCalculation ()
    {
        
    }

    public virtual void CollisionFromChild (Collider2D col)
    {

    }

    public virtual void LifeEnergyCalculate ()
    {
        // float antibiotics = 1f;
        // концентрация антибиотиков
        // if(transform.position.x < -39) antibiotics = 4;
        // else if(transform.position.x < -20) antibiotics = 3;
        // else if(transform.position.x < -1) antibiotics = 2;
        // antibiotics = Mathf.Max(1f, antibiotics - defSkill);
       
    }




    public virtual void Attack (Collision2D col)
    {
        
    }

    public virtual void RotateEnemy()
    {
        
    }

    public virtual void Init(Genome g)
    {
        
    }



    public virtual void Dead()
    {
        
    }

    public virtual void Eat(float food)
    {
        
    }

    public virtual void CreateNewLife (float energy)
    {

    }
        
        

}
