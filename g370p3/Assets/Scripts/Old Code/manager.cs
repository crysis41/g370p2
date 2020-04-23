using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	[Header("Text Control")]
	public Text output;
	public List<string> actions;
	public List<string> logToOutput;
	public Dictionary<string, string> objects;

	[Header("Player")]
	public GameObject currentRoom;

	void Start()
	{
		DisplayRoomText();
		DisplayLoggedText();
	}

	public void AddToLog(string stringToAdd)
	{
		logToOutput.Add(stringToAdd + "\n");
	}

	public void DisplayLoggedText()
	{
		string logAsText = string.Join("\n", logToOutput.ToArray());
		output.text = logAsText;
	}

	public void TakeAction(string[] input)
	{
		switch (input[0])
		{
			case "go":
				AttemptToChangeRooms(input[1]);
				break;
			case "examine":
				if (input.Length == 1 || input[1] == "room")
					DisplayRoomText();
				break;
			default:
				AddToLog("I don't understand that.");
				break;
		}
	}

	public void AttemptToChangeRooms(string direction)
	{
		GameObject nextRoom = null;

		switch (direction)
		{
			case "north":
				nextRoom = currentRoom.GetComponent<Room>().exitRooms[0];
				break;
			case "south":
				nextRoom = currentRoom.GetComponent<Room>().exitRooms[1];
				break;
			case "east":
				nextRoom = currentRoom.GetComponent<Room>().exitRooms[2];
				break;
			case "west":
				nextRoom = currentRoom.GetComponent<Room>().exitRooms[3];
				break;
		}
		if (nextRoom != null && currentRoom.GetComponent<Room>().exitRooms.Contains(nextRoom))
		{
			currentRoom = nextRoom;
			AddToLog("You head off to the " + direction);
			objects = nextRoom.GetComponent<Room>().objects;
			DisplayRoomText();
		}
		else
		{
			AddToLog("There is no path to the " + direction);
		}

	}

	public void DisplayRoomText()
	{
		string outputText = currentRoom.GetComponent<Room>().description + "\n";

		for (int i = 0; i < 4; i++)
		{
			if (currentRoom.GetComponent<Room>().exitDescs[i] != "")
				outputText += "There is " + currentRoom.GetComponent<Room>().exitDescs[i].ToLower();
		}

		AddToLog(outputText);
	}
}