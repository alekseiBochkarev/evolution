using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EvoElement
{
    void AgeCalculation()
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

    private void Eat(float food)
    {
        app.model.enemy.energy += food;
        if (gameObject.GetComponent<EnemyModel>().energy > 16)
        {
            gameObject.GetComponent<EnemyModel>().energy *= 0.5f;
            app.CreateNewLife(gameObject.GetComponent<EnemyModel>().energy, gameObject.GetComponent<EnemyModel>().genome);
        }
    }

    private void Attack(Collision2D col)
    {
        //урон - максимальное из 0 либо атака текущего - защита противоположного юнита
        gameObject.GetComponent<EnemyModel>().damage = Mathf.Max(0f, gameObject.GetComponent<EnemyModel>().attackSkill - col.gameObject.GetComponent<AI>().defSkill);
        gameObject.GetComponent<EnemyModel>().damage *= 4f;
        gameObject.GetComponent<EnemyModel>().damage = Mathf.Min(gameObject.GetComponent<EnemyModel>().damage, col.gameObject.GetComponent<AI>().energy);
        col.gameObject.GetComponent<AI>().energy -= gameObject.GetComponent<EnemyModel>().damage;
        Eat(gameObject.GetComponent<EnemyModel>().damage);
    }



}
