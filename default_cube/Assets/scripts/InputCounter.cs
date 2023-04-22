using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCounter : MonoBehaviour
{
    public int moves_left = 10; // Declare the public variable
    private PlayerMovement playerMovement;
    public LevelController levelController;
    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }


    private void OnEnable()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); // Find the PlayerMovement component
        if (playerMovement != null)
        {
            playerMovement.Assemble += OnAssemble; // Subscribe to the Assemble event
        }
    }

    private void OnDisable()
    {
        if (playerMovement != null)
        {
            playerMovement.Assemble -= OnAssemble; // Unsubscribe from the Assemble event
        }
    }

    private void OnAssemble()
    {
        moves_left--; // Subtract one move with each input
        Debug.Log("Moves left: " + moves_left);

        if (moves_left == 0)
        {
            playerMovement.enabled = false; // Disable the PlayerMovement script
            levelController.LevelReload();
        }
    }
}