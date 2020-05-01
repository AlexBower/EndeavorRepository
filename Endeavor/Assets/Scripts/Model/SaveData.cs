using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public PlayerData playerData;

    public SaveData(PlayerData playerData)
    {
        this.playerData = playerData;
    }
}
