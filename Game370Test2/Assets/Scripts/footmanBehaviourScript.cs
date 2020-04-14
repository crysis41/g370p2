using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footmanBehaviourScript : MonoBehaviour
{
    const float speed = 5.0f;
    public float b;
    public float coin = 18;
    public float timer;
    public float timerLength;
    public GameObject destination;
    public float thirstDesire, foodDesire, entertainmentDesire, restDesire;

    void Start()
    {
        b = Random.Range(1, 100);
        destination = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        GetDesire();

        if (timer <= 0)
            Move();

        CheckLocation();
    }

    void GetDesire()
    {
        //GameObject temp;

        foodDesire = 0.0f;
        thirstDesire = 0.0f;
        entertainmentDesire = 0.0f;
        restDesire = 0.0f;

        foodDesire = GetFoodDesire();
        thirstDesire = GetThirstDesire();
        entertainmentDesire = GetEntertainmentDesire();
        restDesire = GetRestDesire();


        if (foodDesire > thirstDesire && foodDesire > restDesire && foodDesire > entertainmentDesire && coin > 0)
        {
            destination = GameObject.FindGameObjectWithTag("food");
        }
        else if (thirstDesire > foodDesire && thirstDesire > restDesire && thirstDesire > entertainmentDesire && coin > 0)
        {
            destination = GameObject.FindGameObjectWithTag("thirst");
        }
        else if (entertainmentDesire > foodDesire && entertainmentDesire > thirstDesire && entertainmentDesire > restDesire && coin > 0)
        {
            destination = GameObject.FindGameObjectWithTag("entertainment");
        }
        else if (restDesire > foodDesire && restDesire > thirstDesire && restDesire > entertainmentDesire && coin > 0)
        {
            destination = GameObject.FindGameObjectWithTag("rest");
        }
        else
        {
            destination = GameObject.FindGameObjectWithTag("home");
        }
    }

    float GetThirstDesire()
    {
        return 75 * Mathf.Sin(.3f * b);
    }

    float GetFoodDesire()
    {
        return 75 * Mathf.Sin(.15f * b);
    }

    float GetEntertainmentDesire()
	{
        return -1 * Mathf.Pow(b-80, 2) + 100;
    }

    float GetRestDesire()
	{
        return 5 * Mathf.Sqrt(b);
	}

    void Move()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination.transform.position, speed * Time.deltaTime);
    }

    void CheckLocation()
    {
        if (gameObject.transform.position == destination.transform.position)
        {
            float bNew = Random.Range(1, 100);
            while (bNew == b)
            {
                bNew = Random.Range(1, 100);
            }

            coin = coin - 3;

            b = bNew;

            timer = timerLength;
        }
    }
}