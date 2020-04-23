using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonGenerator : MonoBehaviour
{
    public string[] beyondDoor, chambers, doors, exitLocation, exitType, passages, startingArea, cEL, cES, furnishings, chamberState;
    public int[] chamberExitsLarge, chamberExitsSmall;
    public int dungType, rooms, die;

    // Start is called before the first frame update
    void Start()
    {
        dungType = Random.Range(1, 9);
        rooms = Random.Range(4, 30);
        switch (dungType)
        {
            case 1:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\deathTrap.txt");
                break;
            case 2:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\lair.txt");
                break;
            case 3:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\maze.txt");
                break;
            case 4:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\mine.txt");
                break;
            case 5:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\planarGate.txt");
                break;
            case 6:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\stronghold.txt");
                break;
            case 7:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\templeShrine.txt");
                break;
            case 8:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\tomb.txt");
                break;
            case 9:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\treasureVault.txt");
                break;
            case 10:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\furnishings\general.txt");
                break;
        }
        beyondDoor = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\beyondDoor.txt");
        chambers = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\chambers.txt");
        doors = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\doors.txt");
        exitLocation = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\exitLocation.txt");
        exitType = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\exitType.txt");
        passages = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\passages.txt");
        startingArea = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\startingArea.txt");
        chamberState = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\chamberState.txt");
        cEL = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\chamberExitsLarge.txt");
        cES = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\Dungeon Stuff\chamberExitsSmall.txt");

        for (int i = 0; i < cEL.Length; i++)
        {
            chamberExitsLarge[i] = System.Convert.ToInt32(cEL[i]);
        }

        for (int i = 0; i < cES.Length; i++)
        {
            chamberExitsSmall[i] = System.Convert.ToInt32(cES[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rooms; i++)
        {

        }
    }

    void BeyondDoor()
    {

    }
}
