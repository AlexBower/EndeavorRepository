using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingNewArea : MonoBehaviour
{
    private float transitionTime = 0.5f;
    public float startingRGB = 0.22f;
    public float colorChange = 0.02f;
    public int numberOfColorChanges = 8;

    private Camera backgroundCamera;

    private void Start()
    {
        GameManager.instance.SetAreaManager(null);
        backgroundCamera = GetComponent<Camera>();
        StartCoroutine(OpeningFade());
    }

    IEnumerator OpeningFade()
    {
        float currentColor = startingRGB;
        for (int i = 0; i < numberOfColorChanges; i++)
        {
            backgroundCamera.backgroundColor = new Color(currentColor, currentColor, currentColor);
            yield return new WaitForSeconds(transitionTime / numberOfColorChanges);
            currentColor += colorChange;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            player.transform.position = Player.endingLocation;

            PauseMenu.canPauseGame = true;
            SceneManager.LoadScene(Player.newSceneToOpen);
        }
    }
}
