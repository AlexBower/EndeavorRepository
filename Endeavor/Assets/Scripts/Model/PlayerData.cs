using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int[] inventory;
    public Location location;

    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;
        inventory = player.inventory;
        location = new Location(player.transform.position, player.GetDirection());
    }
}
