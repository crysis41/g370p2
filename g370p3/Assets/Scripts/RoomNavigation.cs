﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
	public RoomTutorial currentRoom;

	Dictionary<string, RoomTutorial> exitDictionary = new Dictionary<string, RoomTutorial>();

	GameController controller;

	private void Awake()
	{
		controller = GetComponent<GameController>();
	}

	public void UnpackExitsInRoom()
	{
		for (int i = 0; i < currentRoom.exits; i++)
		{
			exitDictionary.Add(currentRoom.exits.keyString, currentRoom.exits[i].valueRoom);
			controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
		}
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
			controller.LogStringWithReturn("There is no path  to the " + directionNoun);
		}
	}

	public void ClearExits()
	{
		exitDictionary.Clear();
	}
}
