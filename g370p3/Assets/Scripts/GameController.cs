using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public InputAction[] inputActions;

    public Text displayText;

    [HideInInspector] public nameGenerator nmG;

    [HideInInspector] public RoomNavigation roomNavigation;
    public List<string> interactionDescriptionsInRoom = new List<string>();

    List<string> actionLog = new List<string>();

    [HideInInspector] public RoomTutorial[] room;

    [HideInInspector] public string[] beyondDoor, chambers, doors, exitLocation, exitType, passages, startingArea, cEL, cES, furnishings, chamberState;
    [HideInInspector] public int dungeonType, rooms, die, direction, furnished, chamber;
    private string[] directions = new string[4] { "north", "south", "east", "west" };
    private string[] oppDir = new string[4] { "south", "north", "west", "east" };

    // Start is called before the first frame update
    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();

        dungeonType = Random.Range(1, 10);
        rooms = Random.Range(4, 30);
        switch (dungeonType)
        {
            case 1:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\deathTrap.txt");
                break;
            case 2:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\lair.txt");
                break;
            case 3:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\maze.txt");
                break;
            case 4:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\mine.txt");
                break;
            case 5:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\planarGate.txt");
                break;
            case 6:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\stronghold.txt");
                break;
            case 7:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\templeShrine.txt");
                break;
            case 8:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\tomb.txt");
                break;
            case 9:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\treasureVault.txt");
                break;
            case 10:
                furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\general.txt");
                break;
        }
        beyondDoor = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\beyondDoor.txt");
        chambers = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\chambers.txt");
        doors = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\doors.txt");
        exitLocation = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\exitLocation.txt");
        exitType = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\exitType.txt");
        passages = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\passages.txt");
        startingArea = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\startingArea.txt");
        chamberState = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\chamberState.txt");
        cEL = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\chamberExitsLarge.txt");
        cES = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\chamberExitsSmall.txt");

        int[] chamberExitsLarge = new int[cEL.Length], chamberExitsSmall = new int[cES.Length];

        for (int i = 0; i < cEL.Length; i++)
        {
            chamberExitsLarge[i] = System.Convert.ToInt32(cEL[i]);
        }

        for (int i = 0; i < cES.Length; i++)
        {
            chamberExitsSmall[i] = System.Convert.ToInt32(cES[i]);
        }

        CreateRooms();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();

        Debug.Log("DisplayRoomText cleared.");

        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

        LogStringWithReturn(combinedText);
    }

    void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
    }

    void ClearCollectionsForNewRoom()
    {
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    void CreateRooms()
    {
        direction = Random.Range(0, 3);

        room = new RoomTutorial[rooms];

        Debug.Log("Number of rooms: " + rooms);

        for (int i = 0; i < rooms; i++)
        {
            room[i] = ScriptableObject.CreateInstance<RoomTutorial>();
            room[i].roomName = "room" + (i + 1);
        }

        for (int i = 0; i < rooms; i++)
        {
            if (i == 0) // start room
            {
                room[i].exits = new Exit[1];
                room[i].description = startingArea[die];

                string dirWord = directions[direction];

                room[i].exits[0].keyString = dirWord;
                room[i].exits[0].exitDescription = "You see an exit to the " + dirWord + ".";
                room[i].exits[0].valueRoom = room[i + 1];
                Debug.Log("Current room: " + room[i].roomName + "\nNext room: " + room[i].exits[0].valueRoom.roomName);
            }
            else if (i == rooms-1) // end room
            {
                room[i].exits = new Exit[1];
                string dirWord = oppDir[direction];

                room[i].exits[0].keyString = dirWord;
                room[i].exits[0].exitDescription = "You see an exit to the " + dirWord + ".";
                room[i].exits[0].valueRoom = room[i - 1];
            }
            else // literally every other room
            {
                room[i].exits = new Exit[2];
                string dirWord = oppDir[direction];

                room[i].exits[0].keyString = dirWord;
                room[i].exits[0].exitDescription = "You see an exit to the " + dirWord + ".";
                room[i].exits[0].valueRoom = room[i - 1];

                int temp = Random.Range(0, 3);

                while (temp == direction)
                {
                    temp = Random.Range(0, 3);
                }

                direction = temp;

                if (i + 1 == rooms) // for the last room
                {
                    string dirWord2 = oppDir[direction];

                    room[i].exits[1].keyString = dirWord2;
                    room[i].exits[1].exitDescription = "You see an exit to the " + dirWord2 + ".";
                    room[i].exits[1].valueRoom = room[i];
                    Debug.Log("Current room: " + room[i].roomName + "\nNext room: " + room[i].exits[1].valueRoom.roomName);
                }
                else // every other room
                {
                    string dirWord2 = oppDir[direction];

                    room[i].exits[1].keyString = dirWord2;
                    room[i].exits[1].exitDescription = "You see an exit to the " + dirWord2 + ".";
                    room[i].exits[1].valueRoom = room[i + 1];
                    Debug.Log("Current room: " + room[i].roomName + "\nNext room: " + room[i].exits[1].valueRoom.roomName);

                    while ((room[i].exits[0].keyString == "north" && room[i].exits[1].keyString == "south") || (room[i].exits[0].keyString == "south" && room[i].exits[1].keyString == "north"))
                    {
                        if (room[i].exits[0].keyString == room[i].exits[1].keyString)
                        {
                            int temp2 = Random.Range(0, 3);

                            while (temp2 == temp)
                            {
                                temp2 = Random.Range(0, 3);
                            }

                            dirWord2 = oppDir[temp2];
                            room[i].exits[1].keyString = dirWord2;
                            room[i].exits[1].exitDescription = "You see an exit to the " + dirWord2;
                            room[i].exits[1].valueRoom = room[i + 1];
                            Debug.Log("Current room: " + room[i].roomName + "\nNext room: " + room[i].exits[1].valueRoom.roomName);
                        }
                        else
                        {
                            Debug.Log("Loop borked");
                            break;
                        }
                        break;
                    }
                }
            }

            PopulateRooms(i);
        }

        roomNavigation.currentRoom = room[0];
    }
    
    void PopulateRooms(int roomNumber)
    {
        if (roomNumber == 0)
        {
            die = Random.Range(0, startingArea.Length - 1);
            furnished = Random.Range(0, furnishings.Length);
            chamber = Random.Range(0, chamberState.Length - 1);
            room[roomNumber].description = "You enter into a "  + startingArea[die] + ". It appears that this room used to be " + furnishings[furnished] + ", but it is currently " + chamberState[chamber] +".\n";
        }
        else if (roomNumber == rooms-1)
        {
            die = Random.Range(0, chambers.Length - 1);
            furnished = Random.Range(0, furnishings.Length - 1);
            chamber = Random.Range(0, chamberState.Length - 1);
            room[roomNumber].description = "You enter into a " + chambers[die] + ". It appears that this room used to be " + furnishings[furnished] + ", but it is currently " + chamberState[chamber] + ".\nCongratulations! You've found the end!";
        }
        else
        {
            die = Random.Range(0, chambers.Length - 1);
            furnished = Random.Range(0, furnishings.Length - 1);
            chamber = Random.Range(0, chamberState.Length - 1);
            room[roomNumber].description = "You enter into a " + chambers[die] + ". This room appears to have once been " + furnishings[furnished] + ", but it is currently " + chamberState[chamber] + ".";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}