using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    static public string[] savedGamePaths;
    static public int currentGameSaveIndex = 0;

    public int level;
    public int health;
    public int[] inventory;

    private Animator animator;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        
        base.Start();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;
        inventory = data.inventory;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        SceneManager.LoadScene(data.currentScene);
    }

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        savedGamePaths = new string[3];
        savedGamePaths[0] = Application.persistentDataPath + "playerSave0.data";
        savedGamePaths[1] = Application.persistentDataPath + "playerSave1.data";
        savedGamePaths[2] = Application.persistentDataPath + "playerSave2.data";
    }

    void Update()
    {
        if (!GameManager.instance.playersTurn) return;

        if (gameObject.activeSelf && SceneManager.GetActiveScene().name != "MainMenu")
        {
            SavePlayer();
        }

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0)
        {
            // Testing contribution stuff...
            // TODO: call AttemptMove with regards to anything the player may interact with
            Move(horizontal, vertical, out RaycastHit2D hit);
            GameManager.instance.playersTurn = false;
        }
    }

}
