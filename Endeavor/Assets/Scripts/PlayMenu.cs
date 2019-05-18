using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public Player player;

    public void LoadGame(int gameNumberToLoad)
    {
        Player.currentGameSaveIndex = gameNumberToLoad;
        
        if (File.Exists(Player.savedGamePaths[Player.currentGameSaveIndex]))
        {
            player.gameObject.SetActive(true);
            player.LoadPlayer();
        }
        else
        {
            Debug.LogError("Save file not found in " + Player.savedGamePaths[Player.currentGameSaveIndex]);
        }
    }

    public void NewGame(int gameNumberToCreate)
    {
        Player.currentGameSaveIndex = gameNumberToCreate;

        player.gameObject.SetActive(true);
        SceneManager.LoadScene("StartingAlley");
    }
}
