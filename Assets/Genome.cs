using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Genome
{
    public int skillCount = 5;
    //веса - это веса для скалярного произведения (для нейронной сети)
    public float[] weights;
    //скилы - это кормление, атака, защита и размер (может быть от 0 до 4)
    //old code
    public int[] skills;
    //new code
    /*
    public int skill;
    public int size;
    */

    //для инициализации первых бактерий
    public Genome(int size)
    {
        weights = new float[size];
        skills = new int[skillCount];
        for (int i = 0; i < size; i++)
        {
            weights[i] = UnityEngine.Random.Range(-1f, 1f);
        }
    } 

    //копирует веса и скилы родительской бактерии (при инициализации)
    public Genome(Genome a)
    {
        weights = new float[a.weights.Length];
        Array.Copy(a.weights, 0, weights, 0, a.weights.Length);
        skills = new int[skillCount];
        Array.Copy(a.skills, 0, skills, 0, skillCount);
    }
    //если установить жесткие веса - при весе 1 - микробы сильно кучкуются; при 0 - микробы стараются быть поодиночке, при - 0.5 еще больше разделены и двигаются медленно, при 0.5 кучкуются
    public void Mutate(float value)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            if (UnityEngine.Random.value < 0.1)
                weights[i] += UnityEngine.Random.Range(-value, value);
            //то что ниже - это я просо тестировал
            // weights[i] = 0.5f ;
        }
        //old code
        for (int i = 0; i < skillCount; i++)
        {
            if (UnityEngine.Random.value < 0.05)
            {
                skills[i] = UnityEngine.Random.Range(0, 4);
            }
        }
        //new code
        /*
        if (UnityEngine.Random.value < 0.05)
        {
            skill = UnityEngine.Random.Range(0, 4);
            size = UnityEngine.Random.Range(0, 4);
        }
        */
    }
}
