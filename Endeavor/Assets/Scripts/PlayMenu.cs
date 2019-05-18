using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public Player player;

    public void NewGame(int gameNumberToCreate)
    {
        Player.currentGameSaveIndex = gameNumberToCreate;

        player.gameObject.SetActive(true);
        SceneManager.LoadScene("StartingAlley");
    }
}
