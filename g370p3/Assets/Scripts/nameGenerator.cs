using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nameGenerator : MonoBehaviour
{
    public string charName;

    string[] firstName;
    string[] lastName;

    int randFirst, randLast;
    public float timer, timerLength;

    // Start is called before the first frame update
    void Start()
    {
        firstName = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\firstNames.txt");
        lastName = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\lastNames.txt");

        randFirst = Random.Range(0, firstName.Length);
        randLast = Random.Range(0, lastName.Length);

        charName = firstName[randFirst] + " " + lastName[randLast] + "\n";

        gameObject.name = charName;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
            ChangeName();
    }

    void ChangeName()
    {
        randFirst = Random.Range(0, 99);
        randLast = Random.Range(0, 99);

        charName = firstName[randFirst] + " " + lastName[randLast] + "\n";

        gameObject.name = charName;

        timer = timerLength;
    }
}
