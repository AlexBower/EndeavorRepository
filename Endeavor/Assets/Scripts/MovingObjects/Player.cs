using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    static public string[] savedGamePaths;
    static public int currentGameSaveIndex = 0;
    static public bool isChangingArea;
    static public Vector3 endingLocation;
    static public string newSceneToOpen;

    public int level;
    public int health;
    public int[] inventory;

    private GameManager gameManager = null;

    protected override void Start()
    {
        base.Start();
        isChangingArea = true;
        isMoving = false;
        currentlyMovingHorizontal = false;
    }

    public void SavePlayer()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadGame().playerData;

        level = data.level;
        health = data.health;
        inventory = data.inventory;

        SetDirection(data.location.direction);
        transform.position = data.location.GetVector();
        SceneManager.LoadScene(data.location.currentScene);
    }

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        savedGamePaths = new string[3];

        Debug.Log(Application.persistentDataPath);

        savedGamePaths[0] = Application.persistentDataPath + "playerSave0.data";
        savedGamePaths[1] = Application.persistentDataPath + "playerSave1.data";
        savedGamePaths[2] = Application.persistentDataPath + "playerSave2.data";
    }

    void Update()
    {
        gameManager = GameManager.instance;
        if (gameManager.areaManager == null || !gameManager.areaManager.turnBased)
        {
            if (!PauseMenu.gameIsPaused && !isChangingArea)
            {
                TakeRealTimeTurn();
            }
        }
        else if (gameManager.isPlayersTurn && !PauseMenu.gameIsPaused && !isChangingArea)
        {
            TakeTurn();
        }
    }

    public static bool isMoving;

    private void TakeRealTimeTurn()
    {        
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if ((horizontal != 0 || vertical != 0) && !isMoving)
        {
            isMoving = true;

            RaycastHit2D hit;

            if (horizontal != 0)
            {
                if (currentlyMovingHorizontal && vertical != 0)
                {
                    MoveVertical(horizontal, vertical, out hit);
                }
                else
                {
                    MoveHorizontal(horizontal, vertical, out hit);
                }
            }
            else if (vertical != 0)
            {
                if (!currentlyMovingHorizontal && horizontal != 0)
                {
                    MoveHorizontal(horizontal, vertical, out hit);
                }
                else
                {
                    MoveVertical(horizontal, vertical, out hit);
                }
            }
        }
    }

    private void TakeTurn()
    {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0 || vertical != 0)
        {
            RaycastHit2D hit;

            if (horizontal != 0)
            {
                if (currentlyMovingHorizontal && vertical != 0)
                {
                    MoveVertical(horizontal, vertical, out hit);
                }
                else
                {
                    MoveHorizontal(horizontal, vertical, out hit);
                }
            }
            else if (vertical != 0)
            {
                if (!currentlyMovingHorizontal && horizontal != 0)
                {
                    MoveHorizontal(horizontal, vertical, out hit);
                }
                else
                {
                    MoveVertical(horizontal, vertical, out hit);
                }
            }
            gameManager.isPlayersTurn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "AreaTransition")
        {
            isChangingArea = true;

            PauseMenu.canPauseGame = false;
            newSceneToOpen = other.GetComponent<AreaTransition>().newScene;
            endingLocation = other.GetComponent<AreaTransition>().newPosition;
            
            StartCoroutine(DoTransition());
        }
    }

    IEnumerator DoTransition()
    {
        yield return new WaitForSeconds(moveTime / 3);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(moveTime / 6);
        SceneManager.LoadScene("Loading");
    }
}
