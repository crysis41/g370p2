using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nameGenerator : MonoBehaviour
{
    public string charName;

    string[] firstName;
    string[] lastName;

    int randFirst, randLast;

    // Start is called before the first frame update
    void Start()
    {
        firstName = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\firstNames.txt");
        lastName = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\lastNames.txt");

        randFirst = Random.Range(0, 79);
        randLast = Random.Range(0, 79);

        charName = firstName[randFirst] + " " + lastName[randLast] + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
