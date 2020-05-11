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

        player.GetComponent<SpriteRenderer>().enabled = true;

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

        StartCoroutine(GracePeriod());
    }

    IEnumerator GracePeriod()
    {
        Player.isChangingArea = true;
        GameManager.instance.isPlayersTurn = false;
        Player.isMoving = true;
        GameManager.instance.areOthersTakingTurn = true;

        yield return new WaitForSecondsRealtime(0.1f);

        Player.isChangingArea = false;
        GameManager.instance.isPlayersTurn = true;
        Player.isMoving = false;
        GameManager.instance.areOthersTakingTurn = false;
    }

    void LateUpdate()
    {
        if (followCharacter)
        {
            transform.position = player.transform.position + offset;
        }
    }

    public void ForcedUpdate(Vector3 vec)
    {
        transform.position = vec + offset;
    }
}
