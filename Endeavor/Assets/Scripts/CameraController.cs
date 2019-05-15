using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = new Vector3(0.0f, 0.0f, -10.0f);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
