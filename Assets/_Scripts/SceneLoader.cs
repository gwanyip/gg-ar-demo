using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public float delay = 10f;
    public string m_LoadLevelName;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "01_SplashScreen") {
            Debug.Log("Loading next scene after " + delay);
            StartCoroutine(LoadLevelAfterDelay(delay));
        }
    }

    IEnumerator LoadLevelAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(m_LoadLevelName, LoadSceneMode.Single);
    }

    public void SceneSelection(string level) {
        Debug.Log("Loading next scene " + level);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}