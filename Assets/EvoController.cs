using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoController : EvoElement
{
    
    public Vector2 area = new Vector2(10f, 10f);

    public GameObject CreateLife(Vector3 vector3)
    {
        GameObject b = (GameObject)Object.Instantiate(Resources.Load("m3", typeof(GameObject)), vector3, Quaternion.identity);
        b.name = "bacterium";
        return b;
    }

    public void CreateFirstLife()
    {
        Genome genome = new Genome(64);
        Vector3 vector3 = new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0);
        GameObject b = CreateLife(vector3);
        b.GetComponent<EnemyModel>().Init(genome);
    }

    public void StartEvolution()
    {
        //инициализация первых бактерий
        for (int i = 0; i < 10; i++)
        {
            CreateFirstLife();
        }

    }
}
