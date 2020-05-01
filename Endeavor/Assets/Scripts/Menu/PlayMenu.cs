using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
    }

    public void LoadGame(int gameNumberToLoad)
    {
        Player.currentGameSaveIndex = gameNumberToLoad;
        
        if (File.Exists(Player.savedGamePaths[Player.currentGameSaveIndex]))
        {
            player.SetActive(true);
            player.GetComponent<Player>().LoadPlayer();
        }
        else
        {
            Debug.LogError("Save file not found in " + Player.savedGamePaths[Player.currentGameSaveIndex]);
        }
    }

    public void NewGame(int gameNumberToCreate)
    {
        Player.currentGameSaveIndex = gameNumberToCreate;

        player.SetActive(true);
        SceneManager.LoadScene("StartingAlley");
    }
}
