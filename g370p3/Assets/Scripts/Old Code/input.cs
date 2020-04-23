using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input : MonoBehaviour
{
    public InputField inputField;

    Manager m;

    void Awake()
    {
        m = GetComponent<Manager>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        m.AddToLog(userInput);

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        for (int i = 0; i < m.actions.Count; i++)
        {
            if (separatedInputWords[0] == m.actions[i])
            {
                m.TakeAction(separatedInputWords);
            }
        }

        m.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }
}