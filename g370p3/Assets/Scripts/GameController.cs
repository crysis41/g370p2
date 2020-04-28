﻿using System.Collections;
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
    [HideInInspector] public int dungeonType, rooms, die, direction;

    // Start is called before the first frame update
    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();

        dungeonType = Random.Range(1, 10);
        rooms = Random.Range(4, 30);
        //switch (dungeonType)
        //{
        //    case 1:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\deathTrap.txt");
        //        break;
        //    case 2:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\lair.txt");
        //        break;
        //    case 3:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\maze.txt");
        //        break;
        //    case 4:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\mine.txt");
        //        break;
        //    case 5:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\planarGate.txt");
        //        break;
        //    case 6:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\stronghold.txt");
        //        break;
        //    case 7:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\templeShrine.txt");
        //        break;
        //    case 8:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\tomb.txt");
        //        break;
        //    case 9:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\treasureVault.txt");
        //        break;
        //    case 10:
        //        furnishings = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\furnishings\general.txt");
        //        break;
        //}
        beyondDoor = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\beyondDoor.txt");
        chambers = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\chambers.txt");
        doors = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\doors.txt");
        exitLocation = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\exitLocation.txt");
        exitType = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\exitType.txt");
        passages = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\passages.txt");
        startingArea = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\startingArea.txt");
        // chamberState = System.IO.File.ReadAllLines(@"C:\Users\wilso\OneDrive\Documents\GitHub\g370p2\g370p3\Assets\TXTFiles\DungeonStuff\chamberState.txt");
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

        UnpackRoom();

        Debug.Log(roomNavigation.currentRoom.exits[0].exitDescription);

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
        die = Random.Range(0, 9);

        direction = Random.Range(0, 3);
        //roomNavigation.currentRoom.exits = new Exit[1];

        //Debug.Log("Exits array length: " + roomNavigation.currentRoom.exits.Length);

        //switch (direction)
        //{
        //    case 0:
        //        roomNavigation.currentRoom.exits[0].keyString = "north";
        //        roomNavigation.currentRoom.exits[0].exitDescription = "You see an exit to the north.";
        //        break;
        //    case 1:
        //        roomNavigation.currentRoom.exits[0].keyString = "south";
        //        roomNavigation.currentRoom.exits[0].exitDescription = "You see an exit to the south.";
        //        break;
        //    case 2:
        //        roomNavigation.currentRoom.exits[0].keyString = "east";
        //        roomNavigation.currentRoom.exits[0].exitDescription = "You see an exit to the east.";
        //        break;
        //    case 3:
        //        roomNavigation.currentRoom.exits[0].keyString = "west";
        //        roomNavigation.currentRoom.exits[0].exitDescription = "You see an exit to the west.";
        //        break;
        //}

        room = new RoomTutorial[rooms];

        Debug.Log("Number of rooms: " + rooms);

        for (int i = 0; i < rooms; i++)
        {
            room[i] = ScriptableObject.CreateInstance<RoomTutorial>();
            room[i].roomName = "room" + (i + 1);
            if (i == 0)
            {

                room[i].exits = new Exit[1];
                Debug.Log("Exit array length: " + room[i].exits.Length);
                switch (direction)
                {
                    case 0:
                        room[i].exits[0].keyString = "north";
                        room[i].exits[0].exitDescription = "You see an exit to the north.";
                        room[i].exits[0].valueRoom = room[i + 1];
                        break;
                    case 1:
                        room[i].exits[0].keyString = "south";
                        room[i].exits[0].exitDescription = "You see an exit to the south.";
                        room[i].exits[0].valueRoom = room[i + 1];
                        break;
                    case 2:
                        room[i].exits[0].keyString = "east";
                        room[i].exits[0].exitDescription = "You see an exit to the east.";
                        room[i].exits[0].valueRoom = room[i + 1];
                        break;
                    case 3:
                        room[i].exits[0].keyString = "west";
                        room[i].exits[0].exitDescription = "You see an exit to the west.";
                        room[i].exits[0].valueRoom = room[i + 1];
                        break;
                }
            }
            else
            {
                room[i].exits = new Exit[2];

                switch (direction)
                {
                    case 0:
                        room[i].exits[0].keyString = "south";
                        room[i].exits[0].exitDescription = "You see an exit to the south.";
                        room[i].exits[0].valueRoom = room[i - 1];
                        break;
                    case 1:
                        room[i].exits[0].keyString = "north";
                        room[i].exits[0].exitDescription = "You see an exit to the north.";
                        room[i].exits[0].valueRoom = room[i - 1];
                        break;
                    case 2:
                        room[i].exits[0].keyString = "west";
                        room[i].exits[0].exitDescription = "You see an exit to the west.";
                        room[i].exits[0].valueRoom = room[i - 1];
                        break;
                    case 3:
                        room[i].exits[0].keyString = "east";
                        room[i].exits[0].exitDescription = "You see an exit to the east.";
                        room[i].exits[0].valueRoom = room[i - 1];
                        break;
                }

                int temp = Random.Range(0, 3);

                while (temp == direction)
                {
                    temp = Random.Range(0, 3);
                }

                direction = temp;

                switch (direction)
                {
                    case 0:
                        room[i].exits[1].keyString = "north";
                        room[i].exits[1].exitDescription = "You see an exit to the north.";
                        room[i].exits[1].valueRoom = room[i + 1];
                        break;
                    case 1:
                        room[i].exits[1].keyString = "south";
                        room[i].exits[1].exitDescription = "You see an exit to the south.";
                        room[i].exits[1].valueRoom = room[i + 1];
                        break;
                    case 2:
                        room[i].exits[1].keyString = "east";
                        room[i].exits[1].exitDescription = "You see an exit to the east.";
                        room[i].exits[1].valueRoom = room[i + 1];
                        break;
                    case 3:
                        room[i].exits[1].keyString = "west";
                        room[i].exits[1].exitDescription = "You see an exit to the west.";
                        room[i].exits[1].valueRoom = room[i + 1];
                        break;
                }
            }
        }

        roomNavigation.currentRoom = room[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}