using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool gameEnded = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDefeated(string playerID)
    {
        if (gameEnded) return;  // Prevents further processing if the game has ended

        HealthManager[] players = FindObjectsOfType<HealthManager>();
        string winnerID = "";

        foreach (HealthManager player in players)
        {
            if (player.playerID != playerID)
            {
                winnerID = player.playerID;
                gameEnded = true;
                break;
            }
        }

        if (!string.IsNullOrEmpty(winnerID))
        {
            PlayerPrefs.SetString("WinnerID", winnerID);
            PlayerPrefs.Save();
            LoadWinnerScreen();
        }
        else
        {
            Debug.LogError("No winner found.");
        }
    }

    void LoadWinnerScreen()
    {
        SceneManager.LoadScene("Winner");
    }
}
