using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EvoElement
{
    public void AgeCalculation()
    {
        gameObject.GetComponent<EnemyModel>().age += Time.deltaTime;
        //app.model.enemy.age += Time.deltaTime;
    }

    public void Dead()
    {
        for (int i = 0; i < gameObject.GetComponent<EnemyModel>().genome.skillCount; i++)       
        {
            gameObject.GetComponent<EnemyModel>().skillsTotal[app.model.enemy.genome.skills[i]]--;
        }
        //maybe without this
        Destroy(this.gameObject);
    }

    public void Eat(float food)
    {
        gameObject.GetComponent<EnemyModel>().energy += food;
        if (gameObject.GetComponent<EnemyModel>().energy > 16)
        {
            gameObject.GetComponent<EnemyModel>().energy *= 0.5f;
            gameObject.GetComponent<EnemyView>().CreateNewLife(gameObject.GetComponent<EnemyModel>().energy);
            //app.CreateNewLife(gameObject.GetComponent<EnemyModel>().energy, gameObject.GetComponent<EnemyModel>().genome);
        }
    }

    public void Attack(Collision2D col)
    {
        //урон - максимальное из 0 либо атака текущего - защита противоположного юнита
        gameObject.GetComponent<EnemyModel>().damage = Mathf.Max(0f, gameObject.GetComponent<EnemyModel>().attackSkill - col.gameObject.GetComponent<AI>().defSkill);
        gameObject.GetComponent<EnemyModel>().damage *= 4f;
        gameObject.GetComponent<EnemyModel>().damage = Mathf.Min(gameObject.GetComponent<EnemyModel>().damage, col.gameObject.GetComponent<AI>().energy);
        col.gameObject.GetComponent<AI>().energy -= gameObject.GetComponent<EnemyModel>().damage;
        Eat(gameObject.GetComponent<EnemyModel>().damage);
    }

    public void LifeEnergyCalculate()
    {
        // float antibiotics = 1f;
        // концентрация антибиотиков
        // if(transform.position.x < -39) antibiotics = 4;
        // else if(transform.position.x < -20) antibiotics = 3;
        // else if(transform.position.x < -1) antibiotics = 2;
        // antibiotics = Mathf.Max(1f, antibiotics - defSkill);
        gameObject.GetComponent<EnemyModel>().energy -= Time.deltaTime; //* antibiotics * antibiotics;
        if (gameObject.GetComponent<EnemyModel>().energy < 0f)
        {
            Dead();
        }
    }

    public void FindGoalToMove()
    {
        gameObject.GetComponent<EnemyModel>().vision = 5f + gameObject.GetComponent<EnemyModel>().attackSkill;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gameObject.GetComponent<EnemyModel>().vision);

        //neighboursCount = new float[4];
        // вектора к центрам масс еды, красного, зеленого и синего
        gameObject.GetComponent<EnemyModel>().vectors = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            gameObject.GetComponent<EnemyModel>().neighboursCount[i] = 0;
            gameObject.GetComponent<EnemyModel>().vectors[i] = new Vector3(0f, 0f, 0f);
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == gameObject) continue;
            if (colliders[i].gameObject.name == "food")
            {
                gameObject.GetComponent<EnemyModel>().neighboursCount[0]++;
                gameObject.GetComponent<EnemyModel>().vectors[0] += colliders[i].gameObject.transform.position - transform.position;
            }
            else if (colliders[i].gameObject.name == "bacterium")
            {
                AI ai = colliders[i].gameObject.GetComponent<AI>();
                gameObject.GetComponent<EnemyModel>().neighboursCount[1] += ai.attackSkill / 3f;
                gameObject.GetComponent<EnemyModel>().vectors[1] += (colliders[i].gameObject.transform.position - transform.position) * ai.attackSkill;
                gameObject.GetComponent<EnemyModel>().neighboursCount[2] += ai.foodSkill / 3f;
                gameObject.GetComponent<EnemyModel>().vectors[2] += (colliders[i].gameObject.transform.position - transform.position) * ai.foodSkill;
                gameObject.GetComponent<EnemyModel>().neighboursCount[3] += ai.defSkill / 3f;
                gameObject.GetComponent<EnemyModel>().vectors[3] += (colliders[i].gameObject.transform.position - transform.position) * ai.defSkill;
            }
        }
    }

    



}
