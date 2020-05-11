using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaManager : MonoBehaviour
{
    public bool turnBased;
    public static GameObject randomMovingNPC;
    public static GameObject presetMovingNPC;

    public void Start()
    {
        if (randomMovingNPC == null)
        {
            randomMovingNPC = Resources.Load("prefabs/npc/randommovingnpc") as GameObject;
        }
        if (presetMovingNPC == null)
        {
            presetMovingNPC = Resources.Load("prefabs/npc/presetmovingnpc") as GameObject;
        }

        GameManager.instance.SetAreaManager(this);

        LoadNPCs();
    }

    public abstract void LoadNPCs();

    public abstract void TakeNPCTurns();
}
