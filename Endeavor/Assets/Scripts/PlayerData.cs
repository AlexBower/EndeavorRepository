using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public string currentScene;
    public int[] inventory;
    public float[] position;

    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;
        currentScene = SceneManager.GetActiveScene().name;
        inventory = player.inventory;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
