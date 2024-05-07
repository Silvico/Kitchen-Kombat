using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{
    public PlayerInputManager inputManager;
    public GameObject playerA;
    public GameObject playerB;
    public GameObject playerPrefabB;

    void OnPlayerJoined(PlayerInput input)
    {
        if (playerA == null)
        {
            playerA = input.gameObject;
            inputManager.playerPrefab = playerPrefabB;
        }
        else
        {
            playerB = input.gameObject;
        }
    }
}

