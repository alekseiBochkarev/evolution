using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoApplication : MonoBehaviour
{
    //reference to the root instances of the MVC
    public EvoModel model;
    public EvoView view;
    public EvoController controller;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateNewLife(float energy, Genome genome)
    {
        GameObject instance = (GameObject)Object.Instantiate(Resources.Load("m3", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);
        instance.name = "bacterium";
        Genome g = new Genome(genome);
        //при смене значения меняется поведение Мутате (0,5 или 1, или 2) - скорость движения, скорость размножения, кучкование и чем больше значение тем меньше останется зеленых
        g.Mutate(0.5f);
        instance.GetComponent<EnemyModel>().Init(g);
        instance.GetComponent <EnemyModel>().energy = energy;
    }


}

public class EvoElement : MonoBehaviour
{
    //gives access to the application and all instances
    public EvoApplication app { get { return GameObject.FindObjectOfType<EvoApplication>(); } }
}
