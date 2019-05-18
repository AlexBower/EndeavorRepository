using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    [HideInInspector] public bool playersTurn = true;

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
    
    void Update() {
        if (playersTurn || test) {
            return;
        }
        
        StartCoroutine(othersTurn());
    }
public bool test;
    IEnumerator othersTurn() {
        test = true;
        yield return new WaitForSeconds(0.1f);
        playersTurn = true;
        test = false;
    }

}
