using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
	public RoomTutorial currentRoom;

	public Dictionary<string, RoomTutorial> exitDictionary = new Dictionary<string, RoomTutorial>();

	GameController controller;

	private void Awake()
	{
		controller = GetComponent<GameController>();
	}

	public void UnpackExitsInRoom()
	{
		for (int i = 0; i < currentRoom.exits.Length; i++)
		{
			if (!exitDictionary.ContainsKey(currentRoom.exits[i].keyString))
			{
				exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
				controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
			}
		}
		foreach (KeyValuePair<string, RoomTutorial> kvp in exitDictionary)
		{
			Debug.Log("UnpackExitsInRoom dictionary:\nKeyString = " + kvp.Key + ", ValueRoom = " + kvp.Value);
		}

		int exitCount = exitDictionary.Count;

		Debug.Log("exitDictionary length: " + exitCount);
	}

	public void AttemptToChangeRooms(string directionNoun)
	{
		if (exitDictionary.ContainsKey(directionNoun))
		{
			currentRoom = exitDictionary[directionNoun];
			controller.LogStringWithReturn("You head off to the " + directionNoun);
			controller.DisplayRoomText();
		}
		else
		{
			controller.LogStringWithReturn("There is no path to the " + directionNoun);
		}
	}

	public void ClearExits()
	{
		exitDictionary.Clear();
		Debug.Log("Exit dictionary (supposedly) cleared.");
	}
}
