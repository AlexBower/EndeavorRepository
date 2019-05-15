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

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void MoveToNewArea(Vector3 startingPositionInNewArea, string sceneNameOfNewArea)
    {
        transform.position = startingPositionInNewArea;
        SceneManager.LoadScene(sceneNameOfNewArea);
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

    protected override void Start()
    {
        base.Start();
    }

    void Awake()
    {
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
        gameObject.SetActive(false);
    }

    void Update()
    {
// This is just For Testing Save Files! open
        if (gameObject.activeSelf)
        {
            SavePlayer();
        }
        // This is just For Testing Save Files! close

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
            // TODO: call AttemptMove with regards to anything the player may interact with
            AttemptMove<TargetJoint2D>(horizontal, vertical);
        }
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        
    }

}
