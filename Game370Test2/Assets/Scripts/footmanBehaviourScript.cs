using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footmanBehaviourScript : MonoBehaviour
{
    const float speed = 5.0f;
    public float health;

    public GameObject destination;

    public float healthDesire, goldDesire, attackDesire;
    void Start()
    {
        health = 100.0f;
        destination = null;
    }

    // Update is called once per frame
    void Update()
    {
        GetDesire();

        Move();
    }

    void GetDesire()
    {
        goldDesire = 0.0f;
        healthDesire = 0.0f;
        attackDesire = 0.0f;

        healthDesire = GetHealthDesire();
        goldDesire = GetGoldDesire();
        attackDesire = GetAttackDesire();

        if (goldDesire > healthDesire && goldDesire > attackDesire)
        {
            destination = GameObject.FindGameObjectWithTag("chest");
        }
        else if (attackDesire > healthDesire && attackDesire > goldDesire)
        {
            destination = GameObject.FindGameObjectWithTag("orc");
        }
        else if (healthDesire > attackDesire && healthDesire > goldDesire)
        {
            GameObject[] healthList = GameObject.FindGameObjectsWithTag("healthkit");
            destination = healthList[0];
            float closestDistance = Vector3.Distance(gameObject.transform.position, healthList[0].transform.position);

            foreach (GameObject healthUnit in healthList)
            {
                float currentDistance = Vector3.Distance(gameObject.transform.position, healthUnit.transform.position);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    destination = healthUnit;
                }
            }
        }
    }

    float GetHealthDesire()
    {
        return 1000 * Mathf.Exp(-.1f * health);
    }

    float GetGoldDesire()
    {
        return 30;
    }

    float GetAttackDesire()
    {
        return 10 * Mathf.Log(health, 4.2f);
    }

    void Move()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination.transform.position, speed * Time.deltaTime);
    }
}