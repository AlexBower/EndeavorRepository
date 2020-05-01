using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavedGameButtons : MonoBehaviour
{
    public GameObject loadGameButton;
    public GameObject newGameButton;
    [SerializeField]
    public int savedGameButtonsToManipulate;

    void Start()
    {
        if (File.Exists(Player.savedGamePaths[savedGameButtonsToManipulate]))
        {
            loadGameButton.SetActive(true);
            newGameButton.SetActive(false);
        }
        else
        {
            newGameButton.SetActive(true);
            loadGameButton.SetActive(false);
        }
    }
}
