using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> characters = new List<Sprite>();
    private int selectedCharacter = 0; // Keeps track of the selection
    public int playerIndex = 0; // 0 for the first player, 1 for the second

    public void NextOption()
    {
        selectedCharacter = selectedCharacter + 1;
        if (selectedCharacter == characters.Count)
        {
            selectedCharacter = 0;
        }
        sr.sprite = characters[selectedCharacter];
    }

    public void BackOption()
    {
        selectedCharacter = selectedCharacter - 1;
        if (selectedCharacter < 0)
        {
            selectedCharacter = characters.Count - 1;
        }
        sr.sprite = characters[selectedCharacter];
    }

    public void PlayGame()
    {
        SelectionManager.Instance.SetSelectedCharacter(playerIndex, characters[selectedCharacter]);

        if (playerIndex == 0)
        {
            // Load the scene for the second player's character selection
            SceneManager.LoadScene("CharacterSelect2");
        }
        else
        {
            // Both players have chosen, load the main game scene
            SceneManager.LoadScene("SampleScene");
        }
    }
}
