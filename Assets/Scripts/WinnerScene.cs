using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerScene : MonoBehaviour
{
    public GameObject[] winnerPrefabs;  
    public Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    void Start()
    {
        // Initialize the dictionary
        InitializePrefabDictionary();

        string winnerID = PlayerPrefs.GetString("WinnerID", "");

        if (!string.IsNullOrEmpty(winnerID))
        {
            InstantiateWinner(winnerID);
            StartCoroutine(ReturnToCharacterSelection());
        }
        else
        {
            Debug.LogError("Winner ID is null or empty.");
        }
    }

    // Initializes the dictionary mapping string IDs to prefabs
    private void InitializePrefabDictionary()
    {
        prefabDictionary.Add("bananaaa", winnerPrefabs[0]); 
        prefabDictionary.Add("onionnn", winnerPrefabs[1]);
        prefabDictionary.Add("watermelon", winnerPrefabs[2]);
    }

    private void InstantiateWinner(string id)
    {
        if (prefabDictionary.ContainsKey(id))
        {
            Instantiate(prefabDictionary[id], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid winner ID or ID not found in dictionary! ID: " + id);
        }
    }

    IEnumerator ReturnToCharacterSelection()
    {
        yield return new WaitForSeconds(5); 
        SceneManager.LoadScene("CharacterSelect1");  
    }
}
