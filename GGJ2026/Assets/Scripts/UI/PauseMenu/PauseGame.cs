using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    
    private bool paused;

    private void Awake()
    {
        InputSystem.actions["Attack"].Enable();
        InputSystem.actions["Pause"].Enable();
        InputSystem.actions["Pause"].started += PausePressed;
    }

    private void OnDisable()
    {
        InputSystem.actions["Pause"].started -= PausePressed;
        InputSystem.actions["Pause"].Disable();
    }

    private void PausePressed(InputAction.CallbackContext ctx)
    {
        if (!paused)
            Pause();
        else
            UnPause();
    }


    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }
    
    public void UnPause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }
}
