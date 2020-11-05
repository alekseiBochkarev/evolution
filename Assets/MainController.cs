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

    private int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartFood();
        Evolution();
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
    //for bacteriums start value was 100
    private void Evolution()
    {
        for (int i = 0; i < 10; i++)
        {
            Genome genome = new Genome(64);
            GameObject b = Instantiate(bacteriumPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
            b.name = "bacterium";
            b.GetComponent<AI>().Init(genome);
        }
        
    }

     private void StartFood ()
    {
        for (int i = 0; i < 2000; i++)
        {
            GetFood();
        }
    }

    private void GetFood ()
    {   
        GameObject food = Instantiate(foodPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
            food.name = "food";
    }

    void FixedUpdate()
    {
        if(frame % 1 == 0)
        {
            GetFood();
            GetFood();
        }
        frame++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
