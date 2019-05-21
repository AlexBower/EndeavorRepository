using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerPrefab;
    public GameObject gameManagerPrefab;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            player.SetActive(true);
        }

        if (GameManager.instance == null)
        {
            Instantiate(gameManagerPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }

        offset = new Vector3(0.0f, 0.0f, -10.0f);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
