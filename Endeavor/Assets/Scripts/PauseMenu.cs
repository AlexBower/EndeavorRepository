using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool canPauseGame = true;

    public GameObject pauseMenuUI;
    public GameObject player;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PauseMenu");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        DontDestroyOnLoad(this.gameObject);
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
            && !Player.isChangingArea && canPauseGame)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        player.GetComponent<Player>().SavePlayer();
        Cursor.visible = true;
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        player.GetComponent<Player>().SavePlayer();
        Application.Quit();
    }
}

