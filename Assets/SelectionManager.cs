using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    public Sprite[] selectedCharacters = new Sprite[2]; // Array to store the selected sprites

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedCharacter(int playerIndex, Sprite character)
    {
        if (playerIndex >= 0 && playerIndex < selectedCharacters.Length)
        {
            selectedCharacters[playerIndex] = character;
        }
    }
}
