using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingNewArea : MonoBehaviour
{
    public float transitionTime = 1.0f;

    private Camera backgroundCamera;

    private void Start()
    {
        backgroundCamera = GetComponent<Camera>();
        StartCoroutine("OpeningFade");
    }

    IEnumerator OpeningFade()
    {
        backgroundCamera.backgroundColor = new Color(0.3f, 0.3f, 0.3f);
        yield return new WaitForSeconds(transitionTime * 0.25f);
        backgroundCamera.backgroundColor = new Color(0.32f, 0.32f, 0.32f);
        yield return new WaitForSeconds(transitionTime * 0.25f);
        backgroundCamera.backgroundColor = new Color(0.34f, 0.34f, 0.34f);
        yield return new WaitForSeconds(transitionTime * 0.25f);
        backgroundCamera.backgroundColor = new Color(0.36f, 0.36f, 0.36f);
        yield return new WaitForSeconds(transitionTime * 0.25f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = true;
        PauseMenu.canPauseGame = true;
        SceneManager.LoadScene(Player.newSceneToOpen);
    }
}
