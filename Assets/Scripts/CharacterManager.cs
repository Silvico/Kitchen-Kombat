using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> initialCharacters; // Original list for initialization purposes
    private List<Sprite> characters;
    private int selectedCharacter = 0; // Keeps track of the selection
    public int playerIndex = 0; // 0 for the first player, 1 for the second

    private void Start()
    {
        if (playerIndex == 0)
        {
            // First player initializes the shared available characters list
            SelectionManager.Instance.InitializeCharacters(initialCharacters);
        }

        // Update local `characters` to point to the shared `availableCharacters` list
        characters = SelectionManager.Instance.availableCharacters;

        // Ensure that the character selection index doesn't exceed the available characters
        if (characters.Count > 0)
        {
            sr.sprite = characters[selectedCharacter];
        }
    }

    public void NextOption()
    {
        if (characters.Count == 0) return; // If no characters are available, do nothing
        selectedCharacter = (selectedCharacter + 1) % characters.Count;
        sr.sprite = characters[selectedCharacter];
    }

    public void BackOption()
    {
        if (characters.Count == 0) return; // If no characters are available, do nothing
        selectedCharacter = (selectedCharacter - 1 + characters.Count) % characters.Count;
        sr.sprite = characters[selectedCharacter];
    }

    public void PlayGame()
    {
        if (characters.Count == 0) return; // Prevent selection if no characters available
        SelectionManager.Instance.SetSelectedCharacter(playerIndex, characters[selectedCharacter]);

        if (playerIndex == 0)
        {
            // Load the scene for the second player's character selection
            SceneManager.LoadScene("CharacterSelect2_v2");
        }
        else
        {
            // Both players have chosen, load the main game scene
            SceneManager.LoadScene("SampleScene");
        }
    }
}
