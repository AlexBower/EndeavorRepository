using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    [HideInInspector] public bool isPlayersTurn = true;

    public AreaManager areaManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetAreaManager(AreaManager areaManager)
    {
        this.areaManager = areaManager;
    }

    void Update()
    {
        if (!Player.isChangingArea)
        {
            if (areaManager == null || !areaManager.turnBased)
            {
                if (Player.isMoving && isPlayersTurn)
                {
                    StartCoroutine(PlayersRealTime());
                }
                if (!areOthersTakingTurn)
                {
                    StartCoroutine(OthersRealTime());
                }
            }
            else
            {
                if (!isPlayersTurn && !areOthersTakingTurn)
                {
                    StartCoroutine(OthersTurn());
                }
            }
        }
    }

    public bool areOthersTakingTurn = false;

    IEnumerator OthersTurn() {
        areOthersTakingTurn = true;
        
        if (areaManager != null)
        {
            areaManager.TakeNPCTurns();
            // yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(MovingObject.moveTime + 0.05f);

        isPlayersTurn = true;
        areOthersTakingTurn = false;
    }

    IEnumerator OthersRealTime()
    {
        areOthersTakingTurn = true;
        
        if (areaManager != null)
        {
            areaManager.TakeNPCTurns();
            // yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(MovingObject.moveTime + 0.05f);
        areOthersTakingTurn = false;
    }

    IEnumerator PlayersRealTime()
    {
        isPlayersTurn = false;

        yield return new WaitForSeconds(MovingObject.moveTime + 0.05f);

        Player.isMoving = false;

        isPlayersTurn = true;
    }
}
