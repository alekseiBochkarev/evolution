using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove 
{
    void RotateEnemy();
    void FindGoalToMove();
    void Move(NN nn, float[] inputs, float[] neighboursCount, Vector3[] vectors, float vision);

}
