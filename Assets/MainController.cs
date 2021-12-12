using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class MainController : MonoBehaviour
{
    public Vector2 area = new Vector2(10f, 10f);

    //public GameObject bacteriumPrefab;
    public GameObject boidPrefab;
    public GameObject foodPrefab;

    public GameObject finishPanel;
    public Text finishText;
    private string prefabname;

    public int m1Count;
    public float m1MaxAge;
    public int playerCount;
    public float playerMaxAge;

    public enum TypeEnemys
    {
        blade,
        drakula
    }

    public enum Commands
    {
        one = 1,
        two = 2
    }

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
        /* old
        //инициализация первых бактерий
        for (int i = 0; i < 2; i++)
        {
            CreateFirstLife();
        }
        CreatePlayer();
        */
        foreach (Commands command in System.Enum.GetValues(typeof(Commands))) {
            foreach (TypeEnemys typeEnemy in System.Enum.GetValues(typeof(TypeEnemys))) {
                CreateEnemy((int)command, typeEnemy);
            }
        }
    }

    //new
    private void CreateEnemy (int command, TypeEnemys typeEnemy)
    {
        prefabname = typeEnemy.ToString();
        Genome genome = new Genome(64);
        Vector3 vector3 = new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0);
        GameObject b = CreateLife(vector3, prefabname);
        b.GetComponent<AI>().command = command;
        //here we set start charackteristiks
        b.GetComponent<AI>().Init(genome);
        switch (command)
        {
            case 1:
                {
                    playerCount++;
                    break;
                }
            case 2:
                {
                    m1Count++;
                    break;
                }
        }
    }

    /*
    //old
    private void CreatePlayer ()
    {
        prefabname = "blade";
        Genome genome = new Genome(64);
        Vector3 vector3 = new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0);
        GameObject b = CreateLife(vector3, prefabname);
        b.GetComponent<AIPlayer>().Init(genome);
        playerCount++;
    }

    //old
    private void CreateFirstLife ()
    {
        prefabname = "drakula";
        Genome genome = new Genome(64);
        Vector3 vector3 = new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0);
        GameObject b = CreateLife(vector3, prefabname);
        b.GetComponent<AIEnemy>().Init(genome);
        m1Count++;
    }
    */

    private GameObject CreateLife (Vector3 vector3, string name)
    {
        GameObject b = (GameObject)Object.Instantiate(Resources.Load(name, typeof(GameObject)), vector3, Quaternion.identity);
        b.name = name;
        return b;
    }

      private void StartFood ()
    {
        for (int i = 0; i < 200; i++)
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
            //if we want add food
            // SetFood();
           // SetFood();
        }
        frame++;
        if (m1Count <1)
        {
            Win();
        } else if (playerCount <1)
        {
            Lose();
        }
    }

    private void Win ()
    {
        finishPanel.SetActive(true);
        finishText.text = "ты победил";
    }

    private void Lose()
    {
        finishPanel.SetActive(true);
        finishText.text = "ты проиграл";
    }


}
