using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Location
{
    public enum Direction
    {
        STAY, SOUTH, WEST, NORTH, EAST
    }

    public string currentScene;
    private float[] position;
    public Direction direction;

    public Location(Vector3 vec, Direction dir)
    {
        currentScene = SceneManager.GetActiveScene().name;

        position = new float[3];
        position[0] = (int) vec.x;
        position[1] = (int) vec.y;
        position[2] = (int) vec.z;

        direction = dir;
    }

    public Vector3 GetVector()
    {
        Vector3 position;
        position.x = this.position[0];
        position.y = this.position[1];
        position.z = this.position[2];
        return position;
    }
}
