using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerPrefab;
    public GameObject gameManagerPrefab;
    public GameObject pauseMenu;
    public GameObject pauseMenuPrefab;
    public bool followCharacter = true;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            player.SetActive(true);
        }

        if (followCharacter)
        {
            transform.position = player.transform.position + offset;
            offset = new Vector3(0.0f, 0.0f, -10.0f);
        }

        if (GameManager.instance == null)
        {
            Instantiate(gameManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }

        if (GameObject.FindGameObjectWithTag("PauseMenu") == null)
        {
            pauseMenu = Instantiate(pauseMenuPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            pauseMenu.GetComponent<PauseMenu>().player = player;
            pauseMenu.gameObject.SetActive(true);
        }
    }

    void LateUpdate()
    {
        if (followCharacter)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
