using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Vector2 area = new Vector2(10f, 10f);

    public GameObject bacteriumPrefab;
    public GameObject boidPrefab;
    public GameObject foodPrefab;
    private string prefabname;

    public int m1Count;
    public int playerCount;

    private int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartFood();
        StartEvolution();
        //Boids();
    }
    //другой вид бактерий (эксперимент) - не едят друг друга но двигаются стаей
    private void Boids()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject b = Instantiate(boidPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
        }
    }
    //for bacteriums start,after now value was 100
    private void StartEvolution()
    {
        //инициализация первых бактерий
        for (int i = 0; i < 10; i++)
        {
            CreateFirstLife();
        }
        CreatePlayer();
    }

    private void CreatePlayer ()
    {
        prefabname = "player";
        Genome genome = new Genome(64);
        Vector3 vector3 = new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0);
        GameObject b = CreateLife(vector3, prefabname);
        b.GetComponent<AIPlayer>().Init(genome);
        playerCount++;
    }

    private void CreateFirstLife ()
    {
        prefabname = "m1";
        Genome genome = new Genome(64);
        Vector3 vector3 = new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0);
        GameObject b = CreateLife(vector3, prefabname);
        b.GetComponent<AI>().Init(genome);
        m1Count++;
    }

    private GameObject CreateLife (Vector3 vector3, string name)
    {
        GameObject b = (GameObject)Object.Instantiate(Resources.Load(name, typeof(GameObject)), vector3, Quaternion.identity);
        b.name = name;
        return b;
    }

      private void StartFood ()
    {
        for (int i = 0; i < 2000; i++)
        {
            SetFood();
        }
    }

    private void SetFood ()
    {   
        GameObject food = Instantiate(foodPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
            food.name = "food";
    }

    void FixedUpdate()
    {
        if(frame % 1 == 0)
        {
            SetFood();
            SetFood();
        }
        frame++;
    }

    
}
