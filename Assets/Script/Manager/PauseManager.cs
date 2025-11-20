using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    public bool isPaused { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // Lock cursor back
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        LevelManager.instance.LoadScene("MainGame", "CrossFade");
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        LevelManager.instance.LoadScene("MainMenu", "CrossFade");
    }
}
