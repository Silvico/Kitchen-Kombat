using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    // Array to store the selected sprites for each player
    public Sprite[] selectedCharacters = new Sprite[2];

    // Shared list of available characters
    public List<Sprite> availableCharacters;

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

    // Initializes the list of available characters at the start
    public void InitializeCharacters(List<Sprite> characters)
    {
        if (availableCharacters == null || availableCharacters.Count == 0)
        {
            availableCharacters = new List<Sprite>(characters);
        }
    }

    // Set the selected character for a player and remove from available characters
    public void SetSelectedCharacter(int playerIndex, Sprite character)
    {
        if (playerIndex >= 0 && playerIndex < selectedCharacters.Length)
        {
            selectedCharacters[playerIndex] = character;
            availableCharacters.Remove(character);
        }
    }
}
