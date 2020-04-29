using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class RoomTutorial : ScriptableObject
{
    [TextArea]
    public string description;
    public string roomName;

    //public Exit[] exits;
    public Dictionary<string, Exit> exits;
}
